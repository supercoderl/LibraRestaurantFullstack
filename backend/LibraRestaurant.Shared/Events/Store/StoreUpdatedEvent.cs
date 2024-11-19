using System;

namespace LibraRestaurant.Shared.Events.Store;

public sealed class StoreUpdatedEvent : DomainEvent
{

    public StoreUpdatedEvent(Guid storeId) : base(storeId)
    {

    }
}