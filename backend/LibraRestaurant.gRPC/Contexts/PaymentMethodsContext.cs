using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.PaymentMethods;
using LibraRestaurant.Shared.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class PaymentMethodsContext : IPaymentMethodsContext
    {
        private readonly PaymentMethodsApi.PaymentMethodsApiClient _client;

        public PaymentMethodsContext(PaymentMethodsApi.PaymentMethodsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PaymentMethodViewModel>> GetPaymentMethodsByIds(IEnumerable<int> ids)
        {
            var request = new GetPaymentMethodsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.PaymentMethods.Select(paymentMethod => new PaymentMethodViewModel(
                paymentMethod.Id,
                paymentMethod.Name,
                paymentMethod.Description,
                paymentMethod.Picture,
                paymentMethod.IsDeleted));
        }
    }
}
