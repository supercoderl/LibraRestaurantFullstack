using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.PaymentHistories;
using LibraRestaurant.Shared.PaymentHistorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class PaymentHistoriesContext : IPaymentHistoriesContext
    {
        private readonly PaymentHistoriesApi.PaymentHistoriesApiClient _client;

        public PaymentHistoriesContext(PaymentHistoriesApi.PaymentHistoriesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PaymentHistoryViewModel>> GetPaymentHistoriesByIds(IEnumerable<int> ids)
        {
            var request = new GetPaymentHistoriesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.PaymentHistories.Select(paymentHistory => new PaymentHistoryViewModel(
                paymentHistory.PaymentHistoryId,
                paymentHistory.TransactionId,
                Guid.Parse(paymentHistory.OrderId),
                paymentHistory.PaymentMethodId,
                paymentHistory.Amount,
                paymentHistory.CurrencyId,
                paymentHistory.ResponseJSON,
                paymentHistory.CallbackURL,
                DateTime.Parse(paymentHistory.CreatedAt),
                paymentHistory.IsDeleted));
        }
    }
}
