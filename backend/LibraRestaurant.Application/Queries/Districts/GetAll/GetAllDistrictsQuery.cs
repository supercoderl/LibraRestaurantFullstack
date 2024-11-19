using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Districts.GetAll;

public sealed record GetAllDistrictsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    bool IsAll,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<DistrictViewModel>>;