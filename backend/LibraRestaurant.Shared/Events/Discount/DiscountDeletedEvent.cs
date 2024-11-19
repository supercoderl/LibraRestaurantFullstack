using System;

namespace LibraRestaurant.Shared.Events.Discount;

public sealed class DiscountDeletedEvent : DomainEvent
{

    public DiscountDeletedEvent(int discountId) : base(discountId)
    {

    }
}