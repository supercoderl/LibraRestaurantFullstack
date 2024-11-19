using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.OrderLines.GetOrderLineById;

public sealed class GetOrderLineByIdQueryHandler :
    IRequestHandler<GetOrderLineByIdQuery, OrderLineViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IOrderLineRepository _orderLineRepository;

    public GetOrderLineByIdQueryHandler(IOrderLineRepository orderLineRepository, IMediatorHandler bus)
    {
        _orderLineRepository = orderLineRepository;
        _bus = bus;
    }

    public async Task<OrderLineViewModel?> Handle(GetOrderLineByIdQuery request, CancellationToken cancellationToken)
    {
        var orderLine = await _orderLineRepository.GetByIdAsync(request.Id);

        if (orderLine is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetOrderLineByIdQuery),
                    $"OrderLine with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return OrderLineViewModel.FromOrderLine(orderLine);
    }
}