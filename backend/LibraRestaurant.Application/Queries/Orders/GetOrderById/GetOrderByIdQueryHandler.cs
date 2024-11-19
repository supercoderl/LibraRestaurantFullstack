using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.GetOrderById;

public sealed class GetOrderByIdQueryHandler :
    IRequestHandler<GetOrderByIdQuery, OrderViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMediatorHandler bus)
    {
        _orderRepository = orderRepository;
        _bus = bus;
    }

    public async Task<OrderViewModel?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);

        if (order is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetOrderByIdQuery),
                    $"Order with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return OrderViewModel.FromOrder(order);
    }
}