using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Wards;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Wards.GetAll;

public sealed class GetAllWardsQueryHandler :
    IRequestHandler<GetAllWardsQuery, PagedResult<WardViewModel>>
{
    private readonly ISortingExpressionProvider<WardViewModel, Ward> _sortingExpressionProvider;
    private readonly IWardRepository _wardRepository;

    public GetAllWardsQueryHandler(
        IWardRepository wardRepository,
        ISortingExpressionProvider<WardViewModel, Ward> sortingExpressionProvider)
    {
        _wardRepository = wardRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<WardViewModel>> Handle(
        GetAllWardsQuery request,
        CancellationToken cancellationToken)
    {
        var wardsQuery = _wardRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            wardsQuery = wardsQuery.Where(ward =>
                ward.Name.Contains(request.SearchTerm) ||
                (ward.NameEn != null && ward.NameEn.Contains(request.SearchTerm)));
        }

        var totalCount = await wardsQuery.CountAsync(cancellationToken);

        wardsQuery = wardsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        if(!request.IsAll)
        {
            wardsQuery = wardsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);
        }

        var wards = await wardsQuery
            .Select(ward => WardViewModel.FromWard(ward))
            .ToListAsync(cancellationToken);

        return new PagedResult<WardViewModel>(
            totalCount, wards, request.Query.Page, request.Query.PageSize);
    }
}