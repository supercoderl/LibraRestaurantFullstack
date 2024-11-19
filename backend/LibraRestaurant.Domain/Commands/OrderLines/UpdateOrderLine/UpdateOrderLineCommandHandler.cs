
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
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.OrderLine;

namespace LibraRestaurant.Domain.Commands.OrderLines.UpdateOrderLine
{
    public sealed class UpdateOrderLineCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateOrderLineCommand>
    {
        private readonly IOrderLineRepository _orderLineRepository;

        public UpdateOrderLineCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOrderLineRepository orderLineRepository) : base(bus, unitOfWork, notifications)
        {
            _orderLineRepository = orderLineRepository;
        }

        public async Task Handle(UpdateOrderLineCommand request, CancellationToken cancellationToken)
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

            orderLine.SetOrderId(request.OrderId);
            orderLine.SetItemId(request.ItemId);
            orderLine.SetQuantity(request.Quantity);
            orderLine.SetFoodPrice(request.FoodPrice);
            orderLine.SetIsCanceled(request.IsCanceled);
            orderLine.SetCanceledTime(request.CanceledTime);
            orderLine.SetCanceledReason(request.CanceledReason);
            orderLine.SetCustomerReview(request.CustomerReview);
            orderLine.SetCustomerLike(request.CustomerLike);

            _orderLineRepository.Update(orderLine);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrderLineUpdatedEvent(orderLine.OrderLineId));
            }
        }
    }
}
