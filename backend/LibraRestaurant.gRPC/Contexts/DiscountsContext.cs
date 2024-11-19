using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Discounts;
using LibraRestaurant.Shared.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class DiscountsContext : IDiscountsContext
    {
        private readonly DiscountsApi.DiscountsApiClient _client;

        public DiscountsContext(DiscountsApi.DiscountsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<DiscountViewModel>> GetDiscountsByIds(IEnumerable<int> ids)
        {
            var request = new GetDiscountsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Discounts.Select(discount => new DiscountViewModel(
                discount.DiscountId,
                discount.DiscountTypeId,
                discount.CategoryId,
                Guid.Parse(discount.OrderId),
                Guid.Parse(discount.InvoiceId),
                discount.ItemId,
                discount.Comments));
        }
    }
}
