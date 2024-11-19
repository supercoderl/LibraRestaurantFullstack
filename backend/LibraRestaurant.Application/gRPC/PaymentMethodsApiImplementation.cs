using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.PaymentMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class PaymentMethodsApiImplementation : PaymentMethodsApi.PaymentMethodsApiBase
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodsApiImplementation(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public override async Task<GetPaymentMethodsByIdsResult> GetByIds(
            GetPaymentMethodsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var paymentMethods = await _paymentMethodRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(paymentMethod => idsAsIntegers.Contains(paymentMethod.PaymentMethodId))
                .Select(paymentMethod => new GrpcPaymentMethod
                {
                    Id = paymentMethod.PaymentMethodId,
                    Name = paymentMethod.Name,
                    Description = paymentMethod.Description,
                    Picture = paymentMethod.Picture,
                    IsDeleted = paymentMethod.Deleted
                })
                .ToListAsync();

            var result = new GetPaymentMethodsByIdsResult();

            result.PaymentMethods.AddRange(paymentMethods);

            return result;
        }
    }
}
