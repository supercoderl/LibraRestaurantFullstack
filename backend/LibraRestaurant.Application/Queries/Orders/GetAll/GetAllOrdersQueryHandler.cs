using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Orders.GetAll;

public sealed class GetAllOrdersQueryHandler :
    IRequestHandler<GetAllOrdersQuery, PagedResult<OrderViewModel>>
{
    private readonly ISortingExpressionProvider<OrderViewModel, OrderHeader> _sortingExpressionProvider;
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHandler(
        IOrderRepository orderRepository,
        ISortingExpressionProvider<OrderViewModel, OrderHeader> sortingExpressionProvider)
    {
        _orderRepository = orderRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<OrderViewModel>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var ordersQuery = _orderRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .OrderByDescending(x => x.LatestStatusUpdate)
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            ordersQuery = ordersQuery.Where(order =>
                order.OrderNo.Contains(request.SearchTerm) ||
                (order.CustomerNotes != null && order.CustomerNotes.Contains(request.SearchTerm)));
        }

        if(!string.IsNullOrEmpty(request.Phone))
        {
            ordersQuery = ordersQuery.Where(order =>
                (order.Customer != null && order.Customer.Phone.Contains(request.Phone)));
        }

        var totalCount = await ordersQuery.CountAsync(cancellationToken);

        ordersQuery = ordersQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var orders = await ordersQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(order => OrderViewModel.FromOrder(order))
            .ToListAsync(cancellationToken);

        return new PagedResult<OrderViewModel>(
            totalCount, orders, request.Query.Page, request.Query.PageSize);
    }
}