
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.OrderLine;

namespace LibraRestaurant.Domain.Commands.OrderLines.DeleteOrderLine
{
    public sealed class DeleteOrderLineCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteOrderLineCommand>
    {
        private readonly IOrderLineRepository _orderLineRepository;

        public DeleteOrderLineCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOrderLineRepository orderLineRepository) : base(bus, unitOfWork, notifications)
        {
            _orderLineRepository = orderLineRepository;
        }

        public async Task Handle(DeleteOrderLineCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var orderLine = await _orderLineRepository.GetByIdAsync(request.OrderLineId);

            if (orderLine is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no order line with Id {request.OrderLineId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _orderLineRepository.Remove(orderLine);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrderLineDeletedEvent(request.OrderLineId));
            }
        }
    }
}
