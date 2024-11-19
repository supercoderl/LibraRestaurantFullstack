using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.OrderLines.GetAll;

public sealed class GetAllOrderLinesQueryHandler :
    IRequestHandler<GetAllOrderLinesQuery, PagedResult<OrderLineViewModel>>
{
    private readonly ISortingExpressionProvider<OrderLineViewModel, OrderLine> _sortingExpressionProvider;
    private readonly IOrderLineRepository _orderLineRepository;

    public GetAllOrderLinesQueryHandler(
        IOrderLineRepository orderLineRepository,
        ISortingExpressionProvider<OrderLineViewModel, OrderLine> sortingExpressionProvider)
    {
        _orderLineRepository = orderLineRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<OrderLineViewModel>> Handle(
        GetAllOrderLinesQuery request,
        CancellationToken cancellationToken)
    {
        var orderLinesQuery = _orderLineRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        var totalCount = await orderLinesQuery.CountAsync(cancellationToken);

        orderLinesQuery = orderLinesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var orderLines = await orderLinesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(orderLine => OrderLineViewModel.FromOrderLine(orderLine))
            .ToListAsync(cancellationToken);

        return new PagedResult<OrderLineViewModel>(
            totalCount, orderLines, request.Query.Page, request.Query.PageSize);
    }
}