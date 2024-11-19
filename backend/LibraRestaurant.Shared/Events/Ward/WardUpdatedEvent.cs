using System;

namespace LibraRestaurant.Shared.Events.Ward;

public sealed class WardUpdatedEvent : DomainEvent
{

    public WardUpdatedEvent(int wardId) : base(wardId)
    {

    }
}