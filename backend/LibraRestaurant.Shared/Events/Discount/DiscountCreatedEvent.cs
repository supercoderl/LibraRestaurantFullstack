using System;

namespace LibraRestaurant.Shared.Events.Discount;

public sealed class DiscountCreatedEvent : DomainEvent
{
    public DiscountCreatedEvent(int discountId) : base(discountId)
    {
    }
}