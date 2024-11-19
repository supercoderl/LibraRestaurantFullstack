using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.OrderLines;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class OrderLinesApiImplementation : OrderLinesApi.OrderLinesApiBase
    {
        private readonly IOrderLineRepository _orderLineRepository;

        public OrderLinesApiImplementation(IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public override async Task<GetOrderLinesByIdsResult> GetByIds(
            GetOrderLinesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var orderLines = await _orderLineRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(orderLine => idsAsIntegers.Contains(orderLine.OrderLineId))
                .Select(orderLine => new GrpcOrderLine
                {
                    Id = orderLine.OrderLineId,
                    OrderId = orderLine.OrderId.ToString(),
                    ItemId = orderLine.ItemId,
                    Quantity = orderLine.Quantity,
                    IsCanceled = orderLine.IsCanceled,
                    CanceledTime = orderLine.CanceledTime.ToString(),
                    CanceledReason = orderLine.CanceledReason,
                    CustomerReview = orderLine.CustomerReview,
                    IsDeleted = orderLine.Deleted
                })
                .ToListAsync();

            var result = new GetOrderLinesByIdsResult();

            result.OrderLines.AddRange(orderLines);

            return result;
        }
    }
}
