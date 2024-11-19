using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class OrdersApiImplementation : OrdersApi.OrdersApiBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersApiImplementation(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<GetOrdersByIdsResult> GetByIds(
            GetOrdersByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsGuids = new List<Guid>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsGuids.Add(Guid.Parse(id));
            }

            var orders = await _orderRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(order => idsAsGuids.Contains(order.OrderId))
                .Select(order => new GrpcOrder
                {
                    Id = order.OrderId.ToString(),
                    OrderNo = order.OrderNo,
                    StoreId = order.StoreId.ToString(),
                    PaymentMethodId = order.PaymentMethodId ?? 1,
                    PaymentTimeId = order.PaymentTimeId ?? 1,
                    ServantId = order.ServantId.ToString(),
                    CashierId = order.CashierId.ToString(), 
                    CustomerNotes = order.CustomerNotes,    
                    ReservationId = order.ReservationId,
                    PriceCalculated = order.PriceCalculated,
                    PriceAdjustment = order.PriceAdjustment ?? 0,
                    PriceAdjustmentReason = order.PriceAdjustmentReason,
                    Subtotal = order.Subtotal,
                    Tax = order.Tax,
                    Total = order.Total,
                    IsPaid = order.IsPaid,
                    IsPreparationDelayed = order.IsPreparationDelayed,
                    DelayedTime = order.DelayedTime.ToString(),
                    IsCanceled = order.IsCanceled,
                    CanceledTime = order.CanceledTime.ToString(),
                    CanceledReason = order.CanceledReason,
                    IsReady = order.IsReady,
                    ReadyTime = order.ReadyTime.ToString(), 
                    IsCompleted = order.IsCompleted,    
                    CompletedTime = order.CompletedTime.ToString(),
                    IsDeleted = order.Deleted
                })
                .ToListAsync();

            var result = new GetOrdersByIdsResult();

            result.Orders.AddRange(orders);

            return result;
        }
    }
}
