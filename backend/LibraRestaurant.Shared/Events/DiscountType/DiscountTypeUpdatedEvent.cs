using System;

namespace LibraRestaurant.Shared.Events.DiscountType;

public sealed class DiscountTypeUpdatedEvent : DomainEvent
{

    public DiscountTypeUpdatedEvent(int discountTypeId) : base(discountTypeId)
    {

    }
}