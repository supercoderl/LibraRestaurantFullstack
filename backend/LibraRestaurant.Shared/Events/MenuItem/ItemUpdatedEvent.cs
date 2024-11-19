using System;

namespace LibraRestaurant.Shared.Events.MenuItem;

public sealed class ItemUpdatedEvent : DomainEvent
{

    public ItemUpdatedEvent(int itemId) : base(itemId)
    {

    }
}