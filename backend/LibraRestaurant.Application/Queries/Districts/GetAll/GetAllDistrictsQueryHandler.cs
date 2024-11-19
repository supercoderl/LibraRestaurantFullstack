using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Districts.GetAll;

public sealed class GetAllDistrictsQueryHandler :
    IRequestHandler<GetAllDistrictsQuery, PagedResult<DistrictViewModel>>
{
    private readonly ISortingExpressionProvider<DistrictViewModel, District> _sortingExpressionProvider;
    private readonly IDistrictRepository _districtRepository;

    public GetAllDistrictsQueryHandler(
        IDistrictRepository districtRepository,
        ISortingExpressionProvider<DistrictViewModel, District> sortingExpressionProvider)
    {
        _districtRepository = districtRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<DistrictViewModel>> Handle(
        GetAllDistrictsQuery request,
        CancellationToken cancellationToken)
    {
        var districtsQuery = _districtRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            districtsQuery = districtsQuery.Where(district =>
                district.Name.Contains(request.SearchTerm) ||
                (district.NameEn != null && district.NameEn.Contains(request.SearchTerm)));
        }

        var totalCount = await districtsQuery.CountAsync(cancellationToken);

        districtsQuery = districtsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        if(!request.IsAll)
        {
            districtsQuery = districtsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);
        }

        var districts = await districtsQuery
            .Select(district => DistrictViewModel.FromDistrict(district))
            .ToListAsync(cancellationToken);

        return new PagedResult<DistrictViewModel>(
            totalCount, districts, request.Query.Page, request.Query.PageSize);
    }
}