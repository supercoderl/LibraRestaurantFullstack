using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.OrderLines;

public sealed class OrderLineViewModel
{
    public int OrderLineId { get; set; }
    public Guid OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public double FoodPrice { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime? CanceledTime { get; set; }
    public string? CanceledReason { get; set; }
    public string? CustomerReview { get; set; }
    public CustomerLikeStatus CustomerLike { get; set; }

    public static OrderLineViewModel FromOrderLine(OrderLine orderLine)
    {
        return new OrderLineViewModel
        {
            OrderLineId = orderLine.OrderLineId,
            OrderId = orderLine.OrderId,
            ItemId = orderLine.ItemId,
            Quantity = orderLine.Quantity,
            IsCanceled = orderLine.IsCanceled,
            CanceledTime = orderLine.CanceledTime,
            CanceledReason = orderLine.CanceledReason,
            CustomerReview = orderLine.CustomerReview,
            CustomerLike = orderLine.CustomerLike,
        };
    }
}