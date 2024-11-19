using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.DiscountTypes;
using LibraRestaurant.Proto.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class DiscountTypesApiImplementation : DiscountTypesApi.DiscountTypesApiBase
    {
        private readonly IDiscountTypeRepository _discountTypeRepository;

        public DiscountTypesApiImplementation(IDiscountTypeRepository discountTypeRepository)
        {
            _discountTypeRepository = discountTypeRepository;
        }

        public override async Task<GetDiscountTypesByIdsResult> GetByIds(
            GetDiscountTypesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var discountTypes = await _discountTypeRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(discountType => idsAsIntegers.Contains(discountType.DiscountTypeId))
                .Select(discountType => new GrpcDiscountType
                {
                    DiscountTypeId = discountType.DiscountTypeId,
                    Name = discountType.Name,
                    Description = discountType.Description,
                    IsPercentage = discountType.IsPercentage,
                    Value = discountType.Value,
                    CreatedAt = discountType.CreatedAt.ToString(),
                    StartTime = discountType.StartTime.ToString(),
                    EndTime = discountType.EndTime.ToString(),
                    CounponCode = discountType.CounponCode,
                    MinOrderValue = discountType.MinOrderValue,
                    MinItemQuantity = discountType.MinItemQuantity,
                    MaxDiscountValue = discountType.MaxDiscountValue,
                })
                .ToListAsync();

            var result = new GetDiscountTypesByIdsResult();

            result.DiscountTypes.AddRange(discountTypes);

            return result;
        }
    }
}
