using System;

namespace LibraRestaurant.Shared.Events.Store;

public sealed class StoreDeletedEvent : DomainEvent
{

    public StoreDeletedEvent(Guid storeId) : base(storeId)
    {

    }
}