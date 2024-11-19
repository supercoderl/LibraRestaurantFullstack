using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Shared.OrderHeaders;
using System;
using System.Collections.Generic;
using LibraRestaurant.Proto.Orders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class OrdersContext : IOrdersContext
    {
        private readonly OrdersApi.OrdersApiClient _client;

        public OrdersContext(OrdersApi.OrdersApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderHeaderViewModel>> GetOrdersByIds(IEnumerable<Guid> ids)
        {
            var request = new GetOrdersByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Orders.Select(order => new OrderHeaderViewModel(
                Guid.Parse(order.Id),
                order.OrderNo,
                Guid.Parse(order.StoreId),
                order.PaymentMethodId,
                order.PaymentTimeId,
                Guid.Parse(order.ServantId),
                Guid.Parse(order.CashierId),
                order.CustomerNotes,
                order.ReservationId,
                order.PriceCalculated,
                order.PriceAdjustment,
                order.PriceAdjustmentReason,
                order.Subtotal,
                order.Tax,
                order.Total,
                order.CustomerId,
                order.IsPaid,
                order.IsPreparationDelayed,
                DateTime.Parse(order.DelayedTime),
                order.IsCanceled,
                DateTime.Parse(order.CanceledTime),
                order.CanceledReason,
                order.IsReady,
                DateTime.Parse(order.ReadyTime),
                order.IsCompleted,
                DateTime.Parse(order.CompletedTime),
                order.IsDeleted));
        }
    }
}
