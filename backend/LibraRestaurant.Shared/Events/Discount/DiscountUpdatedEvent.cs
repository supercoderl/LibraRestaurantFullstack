using System;

namespace LibraRestaurant.Shared.Events.Discount;

public sealed class DiscountUpdatedEvent : DomainEvent
{

    public DiscountUpdatedEvent(int discountId) : base(discountId)
    {

    }
}