using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Menus.GetAll;

public sealed record GetAllMenusQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<MenuViewModel>>;