using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.GetAll;

public sealed record GetAllOrdersQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    string? Phone = null,
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<OrderViewModel>>;