using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.GetOrderByStoreAndReservation;

public sealed class GetOrderByStoreAndReservationQueryHandler :
    IRequestHandler<GetOrderByStoreAndReservationQuery, OrderViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;

    public GetOrderByStoreAndReservationQueryHandler(IOrderRepository orderRepository, IMediatorHandler bus)
    {
        _orderRepository = orderRepository;
        _bus = bus;
    }

    public async Task<OrderViewModel?> Handle(GetOrderByStoreAndReservationQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByStoreAndReservationAsync(request.StoreId, request.ReservationId);

        if (order is null)
        {
            return null;
        }

        return OrderViewModel.FromOrder(order);
    }
}