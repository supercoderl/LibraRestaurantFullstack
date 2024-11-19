using System;

namespace LibraRestaurant.Shared.Events.Customer;

public sealed class CustomerCreatedEvent : DomainEvent
{
    public CustomerCreatedEvent(int customerId) : base(customerId)
    {
    }
}