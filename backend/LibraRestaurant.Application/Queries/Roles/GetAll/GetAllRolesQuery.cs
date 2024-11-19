using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Roles.GetAll;

public sealed record GetAllRolesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<RoleViewModel>>;