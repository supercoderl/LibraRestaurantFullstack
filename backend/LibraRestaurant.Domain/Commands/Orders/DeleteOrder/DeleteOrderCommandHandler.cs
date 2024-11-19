using LibraRestaurant.Domain.Commands.Menus.DeleteMenu;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.OrderHead;

namespace LibraRestaurant.Domain.Commands.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : CommandHandlerBase,
            IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOrderRepository orderRepository) : base(bus, unitOfWork, notifications)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no order with Id {request.OrderId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _orderRepository.Remove(order);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrderDeletedEvent(request.OrderId));
            }
        }
    }
}
