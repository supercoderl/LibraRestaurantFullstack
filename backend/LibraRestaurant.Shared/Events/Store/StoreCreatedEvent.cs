using System;

namespace LibraRestaurant.Shared.Events.Store;

public sealed class StoreCreatedEvent : DomainEvent
{
    public StoreCreatedEvent(Guid storeId) : base(storeId)
    {
    }
}