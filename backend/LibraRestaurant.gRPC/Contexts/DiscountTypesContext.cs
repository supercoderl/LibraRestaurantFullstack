using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.DiscountTypes;
using LibraRestaurant.Shared.DiscountTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class DiscountTypesContext : IDiscountTypesContext
    {
        private readonly DiscountTypesApi.DiscountTypesApiClient _client;

        public DiscountTypesContext(DiscountTypesApi.DiscountTypesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<DiscountTypeViewModel>> GetDiscountTypesByIds(IEnumerable<int> ids)
        {
            var request = new GetDiscountTypesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.DiscountTypes.Select(discountType => new DiscountTypeViewModel(
                discountType.DiscountTypeId,
                discountType.Name,
                discountType.Description,
                discountType.IsPercentage,
                discountType.Value,
                DateTime.Parse(discountType.CreatedAt),
                DateTime.Parse(discountType.StartTime),
                DateTime.Parse(discountType.EndTime),
                discountType.CounponCode,
                discountType.MinOrderValue,
                discountType.MinItemQuantity,
                discountType.MaxDiscountValue));
        }
    }
}
