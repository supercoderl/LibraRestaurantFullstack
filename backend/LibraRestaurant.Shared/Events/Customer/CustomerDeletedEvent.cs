using System;

namespace LibraRestaurant.Shared.Events.Customer;

public sealed class CustomerDeletedEvent : DomainEvent
{

    public CustomerDeletedEvent(int customerId) : base(customerId)
    {

    }
}