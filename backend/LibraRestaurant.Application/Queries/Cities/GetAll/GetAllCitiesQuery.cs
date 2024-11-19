using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Cities.GetAll;

public sealed record GetAllCitiesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    bool IsAll,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CityViewModel>>;