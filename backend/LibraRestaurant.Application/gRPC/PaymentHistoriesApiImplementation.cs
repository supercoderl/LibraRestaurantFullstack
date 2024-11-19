using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.PaymentHistories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class PaymentHistoriesApiImplementation : PaymentHistoriesApi.PaymentHistoriesApiBase
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

        public PaymentHistoriesApiImplementation(IPaymentHistoryRepository paymentHistoryRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        public override async Task<GetPaymentHistoriesByIdsResult> GetByIds(
            GetPaymentHistoriesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var paymentHistories = await _paymentHistoryRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(paymentHistory => idsAsIntegers.Contains(paymentHistory.PaymentHistoryId))
                .Select(paymentHistory => new GrpcPaymentHistory
                {
                    PaymentHistoryId = paymentHistory.PaymentHistoryId,
                    TransactionId = paymentHistory.TransactionId,
                    OrderId = paymentHistory.OrderId.ToString(),
                    PaymentMethodId = paymentHistory.PaymentMethodId,
                    Amount = paymentHistory.Amount,
                    CurrencyId = paymentHistory.CurrencyId ?? 0,
                    ResponseJSON = paymentHistory.ResponseJSON,
                    CallbackURL = paymentHistory.CallbackURL,
                    CreatedAt = paymentHistory.CreatedAt.ToString(),
                    IsDeleted = paymentHistory.Deleted
                })
                .ToListAsync();

            var result = new GetPaymentHistoriesByIdsResult();

            result.PaymentHistories.AddRange(paymentHistories);

            return result;
        }
    }
}
