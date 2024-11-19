using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Domain.Commands.Orders.CreateOrder;
using LibraRestaurant.Domain.Commands.Orders.UpdateOrder;
using LibraRestaurant.Domain.Commands.Orders.DeleteOrder;
using LibraRestaurant.Application.Queries.Orders.GetOrderById;
using LibraRestaurant.Application.Queries.Orders.GetAll;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Application.Queries.Orders.GetOrderByStoreAndReservation;
using LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine;
using System.Linq;

namespace LibraRestaurant.Application.Services
{
    public sealed class OrderService : IOrderService
    {
        private readonly IMediatorHandler _bus;

        public OrderService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<OrderViewModel?> GetOrderByIdAsync(Guid orderId)
        {
            return await _bus.QueryAsync(new GetOrderByIdQuery(orderId));
        }

        public async Task<PagedResult<OrderViewModel>> GetAllOrdersAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            string? phone = null,
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllOrdersQuery(query, includeDeleted, searchTerm, phone, sortQuery));
        }

        public async Task<PagedResult<OrderViewModel>> GetOrdersByPhoneAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            string? phone = null,
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllOrdersQuery(query, includeDeleted, searchTerm, phone, sortQuery));
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderViewModel order)
        {
            Guid? id = await CheckOrderIsReady(order.StoreId, order.ReservationId);

            if (id is not null)
            {
                return id.Value;
            }

            id = Guid.NewGuid();

            await _bus.SendCommandAsync(new CreateOrderCommand(
            id.Value,
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
            order.CustomerId,
            order.LatestStatus,
            order.LatestStatusUpdate,
            order.IsPaid,
            order.IsPreparationDelayed,
            order.DelayedTime,
            order.IsCanceled,
            order.CaceledTime,
            order.CanceledReason,
            order.IsReady,
            order.ReadyTime,
            order.IsCompleted,
            order.CompletedTime,
            order.OrderLines.Select(item => new CreateOrderLineCommand(
                0,
                (Guid)id,
                item.ItemId,
                item.Quantity,
                item.FoodPrice,
                item.IsCanceled,
                item.CanceledTime,
                item.CanceledReason,
                item.CustomerReview,
                item.CustomerLike
            )).ToList()));

            return id.Value;
        }

        public async Task UpdateOrderAsync(UpdateOrderViewModel order)
        {
            await _bus.SendCommandAsync(new UpdateOrderCommand(
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
                order.LatestStatus,
                order.LatestStatusUpdate,
                order.IsPaid,
                order.IsPreparationDelayed,
                order.DelayedTime,
                order.IsCanceled,
                order.CaceledTime,
                order.CanceledReason,
                order.IsReady,
                order.ReadyTime,
                order.IsCompleted,
                order.CompletedTime,
                order.OrderLines.Select(item => new CreateOrderLineCommand(
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
                )).ToList(),
                order.Action));
        }

        public async Task UpdatePaymentMethodAsync(Guid orderId, int paymentMethodId)
        {
            var order = await _bus.QueryAsync(new GetOrderByIdQuery(orderId));

            if (order is not null)
            {
                await _bus.SendCommandAsync(new UpdateOrderCommand(
                order.OrderId,
                order.OrderNo,
                order.StoreId,
                paymentMethodId,
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
                OrderStatus.Ready,
                DateTime.Now,
                order.IsPaid,
                order.IsPreparationDelayed,
                order.DelayedTime,
                order.IsCanceled,
                order.CanceledTime,
                order.CanceledReason,
                true,
                DateTime.Now,
                order.IsCompleted,
                order.CompletedTime,
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
                "update"));
            }
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            await _bus.SendCommandAsync(new DeleteOrderCommand(orderId));
        }

        private async Task<Guid?> CheckOrderIsReady(Guid storeId, int reservationId)
        {
            var order = await _bus.QueryAsync(new GetOrderByStoreAndReservationQuery(storeId, reservationId));
            if (order is not null) return order.OrderId;
            return null;
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
        {
            var order = await _bus.QueryAsync(new GetOrderByIdQuery(orderId));
            if (order is null) return;
            await _bus.SendCommandAsync(new UpdateOrderCommand(
                    orderId,
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
                    status,
                    DateTime.Now,
                    order.IsPaid,
                    order.IsPreparationDelayed,
                    order.DelayedTime,
                    order.IsCanceled,
                    order.CanceledTime,
                    order.CanceledReason,
                    order.IsReady,
                    order.ReadyTime,
                    order.IsCompleted,
                    order.CompletedTime,
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
                    "update"
            ));
        }
    }
}
