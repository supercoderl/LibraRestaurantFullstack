using System;

namespace LibraRestaurant.Shared.Events.DiscountType;

public sealed class DiscountTypeCreatedEvent : DomainEvent
{
    public DiscountTypeCreatedEvent(int discountTypeId) : base(discountTypeId)
    {
    }
}