using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.GetOrdersByPhone;

public sealed record GetOrdersByPhoneQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    string? Phone = null,
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<OrderViewModel>>;