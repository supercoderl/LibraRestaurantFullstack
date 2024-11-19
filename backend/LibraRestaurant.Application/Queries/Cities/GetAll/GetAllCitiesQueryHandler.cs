using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Cities.GetAll;

public sealed class GetAllCitiesQueryHandler :
    IRequestHandler<GetAllCitiesQuery, PagedResult<CityViewModel>>
{
    private readonly ISortingExpressionProvider<CityViewModel, City> _sortingExpressionProvider;
    private readonly ICityRepository _cityRepository;

    public GetAllCitiesQueryHandler(
        ICityRepository cityRepository,
        ISortingExpressionProvider<CityViewModel, City> sortingExpressionProvider)
    {
        _cityRepository = cityRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CityViewModel>> Handle(
        GetAllCitiesQuery request,
        CancellationToken cancellationToken)
    {
        var citiesQuery = _cityRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            citiesQuery = citiesQuery.Where(city =>
                city.Name.Contains(request.SearchTerm) ||
                (city.NameEn != null && city.NameEn.Contains(request.SearchTerm)));
        }

        var totalCount = await citiesQuery.CountAsync(cancellationToken);

        citiesQuery = citiesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        if(!request.IsAll)
        {
            citiesQuery = citiesQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);
        }

        var cities = await citiesQuery
            .Select(city => CityViewModel.FromCity(city))
            .ToListAsync(cancellationToken);

        return new PagedResult<CityViewModel>(
            totalCount, cities, request.Query.Page, request.Query.PageSize);
    }
}