
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.PaymentHistory;
using LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine;
using LibraRestaurant.Domain.Commands.Orders.UpdateOrder;
using LibraRestaurant.Domain.Enums;
using static LibraRestaurant.Domain.Errors.DomainErrorCodes;
using System;
using System.Linq;

namespace LibraRestaurant.Domain.Commands.PaymentHistories.CreatePaymentHistory
{
    public sealed class CreatePaymentHistoryCommandHandler : CommandHandlerBase,
        IRequestHandler<CreatePaymentHistoryCommand>
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IOrderRepository _orderRepository;

        public CreatePaymentHistoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPaymentHistoryRepository paymentHistoryRepository,
            IOrderRepository orderRepository) : base(bus, unitOfWork, notifications)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(CreatePaymentHistoryCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var paymentHistory = new Entities.PaymentHistory(
                request.PaymentHistoryId,
                request.TransactionId,
                request.OrderId,
                request.PaymentMethodId,
                request.Amount,
                request.CurrencyId,
                request.ResponseJSON,
                request.CallbackURL,
                request.CreatedAt,
                request.Status);

            _paymentHistoryRepository.Add(paymentHistory);

            await UpdateOrder(Bus, request.OrderId);


            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PaymentHistoryCreatedEvent(paymentHistory.PaymentHistoryId));
            }
        }

        private async Task UpdateOrder(IMediatorHandler Bus, Guid OrderId)
        {
            var order = await _orderRepository.GetByIdAsync(OrderId);

            if (order is not null)
            {
                await Bus.SendCommandAsync(new UpdateOrderCommand(
                    order.OrderId,
                    order.OrderNo,
                    order.StoreId,
                    order.PaymentMethodId,
                    order.PaymentTimeId,
                    order.ServantId,
                    order.CashierId,
                    order.CustomerNotes,
                    order.ReservationId,
                    order.PriceCalculated,
                    order.PriceAdjustment,
                    order.PriceAdjustmentReason,
                    order.Subtotal,
                    order.Tax,
                    order.Total,
                    OrderStatus.Paid,
                    DateTime.Now,
                    true,
                    false,
                    order.DelayedTime,
                    false,
                    order.CanceledTime,
                    order.CanceledReason,
                    false,
                    DateTime.Now,
                    true,
                    DateTime.Now,
                    order.OrderLines is not null ? order.OrderLines.Select(item => new CreateOrderLineCommand(
                        0,
                        order.OrderId,
                        item.ItemId,
                        item.Quantity,
                        item.FoodPrice,
                        item.IsCanceled,
                        item.CanceledTime,
                        item.CanceledReason,
                        item.CustomerReview,
                        item.CustomerLike
                    )).ToList() : new System.Collections.Generic.List<CreateOrderLineCommand>(),
                    "pay"));
            }
        }
    }
}
