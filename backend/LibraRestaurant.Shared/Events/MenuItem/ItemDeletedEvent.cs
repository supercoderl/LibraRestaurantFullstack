using System;

namespace LibraRestaurant.Shared.Events.MenuItem;

public sealed class ItemDeletedEvent : DomainEvent
{

    public ItemDeletedEvent(int itemId) : base(itemId)
    {

    }
}