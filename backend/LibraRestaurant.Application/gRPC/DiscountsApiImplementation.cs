using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Discounts;
using LibraRestaurant.Proto.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class DiscountsApiImplementation : DiscountsApi.DiscountsApiBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountsApiImplementation(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public override async Task<GetDiscountsByIdsResult> GetByIds(
            GetDiscountsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var discounts = await _discountRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(discount => idsAsIntegers.Contains(discount.DiscountId))
                .Select(discount => new GrpcDiscount
                {
                    DiscountId = discount.DiscountId,
                    DiscountTypeId = discount.DiscountTypeId,
                    CategoryId = discount.CategoryId ?? 0,
                    ItemId = discount.ItemId ?? 0,
                    OrderId = discount.OrderId.ToString(),
                    InvoiceId = discount.InvoiceId.ToString(),
                    Comments = discount.Comments
                })
                .ToListAsync();

            var result = new GetDiscountsByIdsResult();

            result.Discounts.AddRange(discounts);

            return result;
        }
    }
}
