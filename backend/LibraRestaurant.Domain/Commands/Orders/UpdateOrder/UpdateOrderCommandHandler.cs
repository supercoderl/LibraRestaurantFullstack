using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
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
using LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine;
using System.ComponentModel.Design;

namespace LibraRestaurant.Domain.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IReservationRepository _reservationRepository;

        public UpdateOrderCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOrderRepository orderRepository,
            IOrderLineRepository orderLineRepository,
            IReservationRepository reservationRepository) : base(bus, unitOfWork, notifications)
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
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

            if(request.OrderNo is not null)
            {
                order.SetOrderNo(request.OrderNo);
            }
            order.SetStoreId(request.StoreId);
            order.SetPaymentMethodId(request.PaymentMethodId);
            order.SetPaymentTimeId(request.PaymentTimeId);
            order.SetServantId(request.ServantId);
            order.SetCashierId(request.CashierId);
            order.SetCustomerNotes(request.CustomerNotes);
            order.SetReservationId(request.ReservationId);
            order.SetPriceCalculated(request.PriceCalculated);
            order.SetPriceAdjustment(request.PriceAdjustment);
            order.SetPriceAdjustmentReason(request.PriceAdjustmentReason);
            order.SetSubtotal(request.Subtotal);
            order.SetTax(request.Tax);
            order.SetTotal(request.Total);
            order.SetLatestStatus(request.LatestStatus);
            order.SetLatestStatusUpdate(request.LatestStatusUpdate);
            order.SetIsPaid(request.IsPaid);
            order.SetIsPreparationDelayed(request.IsPreparationDelayed);
            order.SetDelayTime(request.DelayedTime);
            order.SetIsCanceled(request.IsCanceled);
            order.SetCancelTime(request.CanceledTime);
            order.SetCanceledReason(request.CanceledReason);
            order.SetIsReady(request.IsReady);
            order.SetReadyTime(request.ReadyTime);
            order.SetIsCompleted(request.IsCompleted);
            order.SetCompletedTime(request.CompletedTime);

            _orderRepository.Update(order);

            await AddItems(request.OrderLines, order.OrderId);

            if(request.Action == "pay" || request.Action == "cancel")
            {
                await UpdateReservation(request.ReservationId, request.Action);
            }

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new OrderUpdatedEvent(order.OrderId));
            }
        }

        private async Task AddItems(List<CreateOrderLineCommand> OrderLines, Guid OrderId)
        {
            foreach (var item in OrderLines)
            {
                // Kiểm tra xem OrderLine đã tồn tại trong DB chưa
                var existingOrderLine = await _orderLineRepository.GetByOrderAndItemAsync(OrderId, item.ItemId);

                if (existingOrderLine != null)
                {
                    // Nếu tồn tại, kiểm tra quantity
                    if (existingOrderLine.Quantity < item.Quantity)
                    {
                        // Nếu quantity trong DB nhỏ hơn quantity truyền vào, cập nhật giá trị
                        existingOrderLine.SetQuantity(item.Quantity);
                        _orderLineRepository.Update(existingOrderLine); // Cập nhật lại
                    }
                }
                else
                {
                    // Nếu không có trong DB, thêm mới
                    var orderItem = new Entities.OrderLine(
                        item.OrderLineId,
                        OrderId,
                        item.ItemId,
                        item.Quantity,
                        item.FoodPrice,
                        item.IsCanceled,
                        item.CanceledTime,
                        item.CanceledReason,
                        item.CustomerReview,
                        item.CustomerLike
                    );

                    _orderLineRepository.Add(orderItem); // Thêm mới
                }
            }
        }

        private async Task UpdateReservation(int ReservationId, string Action)
        {
            var reservation = await _reservationRepository.GetByIdAsync(ReservationId);
            if (reservation is not null)
            {
                if(Action == "pay")
                {
                    reservation.SetStatus(Enums.ReservationStatus.Cleaning);
                    reservation.SetCleaningTime(DateTime.Now);
                }
                else
                {
                    reservation.SetStatus(Enums.ReservationStatus.Available);
                }
                
                _reservationRepository.Update(reservation);
            }
        }
    }
}
