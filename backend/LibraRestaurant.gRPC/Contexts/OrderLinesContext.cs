using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.OrderLines;
using LibraRestaurant.Shared.CustomerLines;
using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class OrderLinesContext : IOrderLinesContext
    {
        private readonly OrderLinesApi.OrderLinesApiClient _client;

        public OrderLinesContext(OrderLinesApi.OrderLinesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderLineViewModel>> GetOrderLinesByIds(IEnumerable<int> ids)
        {
            var request = new GetOrderLinesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.OrderLines.Select(orderLine => new OrderLineViewModel(
                orderLine.Id,
                Guid.Parse(orderLine.OrderId),
                orderLine.ItemId,
                orderLine.Quantity,
                orderLine.FoodPrice,
                orderLine.IsCanceled,
                DateTime.Parse(orderLine.CanceledTime),
                orderLine.CanceledReason,
                orderLine.CustomerReview,
                orderLine.IsDeleted));
        }
    }
}
