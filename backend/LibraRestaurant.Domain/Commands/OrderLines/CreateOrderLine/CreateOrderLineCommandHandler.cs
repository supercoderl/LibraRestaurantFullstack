
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.OrderLine;

namespace LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine
{
    public sealed class CreateOrderLineCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateOrderLineCommand>
    {
        private readonly IOrderLineRepository _orderLineRepository;

        public CreateOrderLineCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOrderLineRepository orderLineRepository) : base(bus, unitOfWork, notifications)
        {
            _orderLineRepository = orderLineRepository;
        }

        public async Task Handle(CreateOrderLineCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var orderLine = new Entities.OrderLine(
                request.OrderLineId,
                request.OrderId,
                request.ItemId,
                request.Quantity,
                request.FoodPrice,
                request.IsCanceled,
                request.CanceledTime,
                request.CanceledReason,
                request.CustomerReview,
                request.CustomerLike);

            _orderLineRepository.Add(orderLine);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrderLineCreatedEvent(orderLine.OrderLineId));
            }
        }
    }
}
