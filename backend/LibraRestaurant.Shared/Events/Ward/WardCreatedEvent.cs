using System;

namespace LibraRestaurant.Shared.Events.Ward;

public sealed class WardCreatedEvent : DomainEvent
{
    public WardCreatedEvent(int wardId) : base(wardId)
    {
    }
}