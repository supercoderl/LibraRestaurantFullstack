using System;

namespace LibraRestaurant.Shared.Events.Ward;

public sealed class WardDeletedEvent : DomainEvent
{

    public WardDeletedEvent(int wardId) : base(wardId)
    {

    }
}