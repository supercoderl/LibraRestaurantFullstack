using System;
using System.Collections.Generic;
using System.Linq;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Application.ViewModels.Payments;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Orders;

public sealed class OrderViewModel
{
    public Guid OrderId { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public int? PaymentMethodId { get; set; }
    public int? PaymentTimeId { get; set; }
    public Guid? ServantId { get; set; }
    public Guid? CashierId { get; set; }
    public string? CustomerNotes { get; set; }
    public int ReservationId { get; set; }
    public double PriceCalculated { get; set; }
    public double? PriceAdjustment { get; set; }
    public string? PriceAdjustmentReason { get; set; }
    public double Subtotal { get; set; }
    public double Tax { get; set; }
    public double Total { get; set; }
    public OrderStatus LatestStatus { get; set; }
    public DateTime LatestStatusUpdate { get; set; }
    public bool IsPaid { get; set; }
    public bool IsPreparationDelayed { get; set; }
    public DateTime? DelayedTime { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime? CanceledTime { get; set; }
    public string? CanceledReason { get; set; }
    public bool IsReady { get; set; }
    public DateTime? ReadyTime { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedTime { get; set; }
    public string? StoreName { get; set; }
    public List<OrderLogFromOrderViewModel>? OrderLogs { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }

    public List<OrderLineFromOrderViewModel>? OrderLines { get; set; }

    public static OrderViewModel FromOrder(OrderHeader order)
    {
        return new OrderViewModel
        {
            OrderId = order.OrderId,
            OrderNo = order.OrderNo,
            StoreId = order.StoreId,
            PaymentMethodId = order.PaymentMethodId,
            PaymentTimeId = order.PaymentTimeId,
            ServantId = order.ServantId,
            CashierId = order.CashierId,
            CustomerNotes = order.CustomerNotes,
            ReservationId = order.ReservationId,
            PriceCalculated = order.PriceCalculated,
            PriceAdjustment = order.PriceAdjustment,
            PriceAdjustmentReason = order.PriceAdjustmentReason,
            Subtotal = order.Subtotal,
            Tax = order.Tax,
            Total = order.Total,
            LatestStatus = order.LatestStatus,
            LatestStatusUpdate = order.LatestStatusUpdate,
            IsPaid = order.IsPaid,
            IsPreparationDelayed = order.IsPreparationDelayed,
            DelayedTime = order.DelayedTime,
            IsCanceled = order.IsCanceled,
            CanceledTime = order.CanceledTime,
            CanceledReason = order.CanceledReason,
            IsReady = order.IsReady,
            ReadyTime = order.ReadyTime,
            IsCompleted = order.IsCompleted,
            CompletedTime = order.CompletedTime,
            StoreName = order?.Store?.Name,
            OrderLines = GenerateOrderLines(order?.OrderLines),
            OrderLogs = GenerateOrderLogs(order?.OrderLogs),
            CustomerName = order?.Customer?.Name,
            CustomerPhone = order?.Customer?.Phone
        };
    }

    private static List<OrderLineFromOrderViewModel>? GenerateOrderLines(IEnumerable<OrderLine>? orderLines)
    {
        if (orderLines is null || !orderLines.Any()) return null;

        return orderLines
            .Select(item =>
            {
                return new OrderLineFromOrderViewModel
                {
                    OrderLineId = item.OrderLineId,
                    OrderId = item.OrderId,
                    ItemId = item.ItemId,
                    FoodName = item.Item?.Title,
                    Quantity = item.Quantity,
                    IsCanceled = item.IsCanceled,
                    CanceledTime = item.CanceledTime,
                    CanceledReason = item.CanceledReason,
                    CustomerReview = item.CustomerReview,
                    CustomerLike = item.CustomerLike,
                    FoodPrice = item.FoodPrice
                };
            }).ToList();
    }

    private static List<OrderLogFromOrderViewModel>? GenerateOrderLogs(IEnumerable<OrderLog>? orderLogs)
    {
        if (orderLogs == null) return null;

        return orderLogs
            .OrderBy(log => log.Time)
            .GroupBy(log => log.ItemId)
            .Select(group => new OrderLogFromOrderViewModel
            {
                ItemId = group.Key,
                QuantityChanges = string.Join(" → ", group.Select(log => log.NewQuantity)),
                TimeChanges = string.Join(" → ", group.Select(log => log.Time.ToString("HH:mm")))
            })
            .ToList();
    }
}