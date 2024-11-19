using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.OrderLines.GetOrderLineByOrderAndItem;

public sealed class GetOrderLineByOrderAndItemQueryHandler :
    IRequestHandler<GetOrderLineByOrderAndItemQuery, OrderLineViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IOrderLineRepository _orderLineRepository;

    public GetOrderLineByOrderAndItemQueryHandler(IOrderLineRepository orderLineRepository, IMediatorHandler bus)
    {
        _orderLineRepository = orderLineRepository;
        _bus = bus;
    }

    public async Task<OrderLineViewModel?> Handle(GetOrderLineByOrderAndItemQuery request, CancellationToken cancellationToken)
    {
        var orderLine = await _orderLineRepository.GetByOrderAndItemAsync(request.OrderId, request.ItemId);

        if (orderLine is null)
        {
            return null;
        }

        return OrderLineViewModel.FromOrderLine(orderLine);
    }
}