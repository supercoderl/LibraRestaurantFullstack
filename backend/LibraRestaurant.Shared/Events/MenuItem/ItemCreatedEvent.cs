using System;

namespace LibraRestaurant.Shared.Events.MenuItem;

public sealed class ItemCreatedEvent : DomainEvent
{
    public ItemCreatedEvent(int itemId) : base(itemId)
    {
    }
}