using System;

namespace LibraRestaurant.Shared.Events.DiscountType;

public sealed class DiscountTypeDeletedEvent : DomainEvent
{

    public DiscountTypeDeletedEvent(int discountTypeId) : base(discountTypeId)
    {

    }
}