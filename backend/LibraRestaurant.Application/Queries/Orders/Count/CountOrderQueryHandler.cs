using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.CountOrder;

public sealed class CountOrderQueryHandler :
    IRequestHandler<CountOrderQuery, int>
{
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;

    public CountOrderQueryHandler(IOrderRepository orderRepository, IMediatorHandler bus)
    {
        _orderRepository = orderRepository;
        _bus = bus;
    }

    public async Task<int> Handle(CountOrderQuery request, CancellationToken cancellationToken)
    {
        var num = await _orderRepository.CountOrderAsync(request.Month, request.Year);

        return num;
    }
}