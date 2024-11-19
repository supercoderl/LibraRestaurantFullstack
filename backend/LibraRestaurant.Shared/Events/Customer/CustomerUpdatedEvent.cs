using System;

namespace LibraRestaurant.Shared.Events.Customer;

public sealed class CustomerUpdatedEvent : DomainEvent
{

    public CustomerUpdatedEvent(int customerId) : base(customerId)
    {

    }
}