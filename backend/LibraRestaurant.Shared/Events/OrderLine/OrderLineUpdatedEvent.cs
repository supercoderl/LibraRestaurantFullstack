using System;

namespace LibraRestaurant.Shared.Events.OrderLine;

public sealed class OrderLineUpdatedEvent : DomainEvent
{

    public OrderLineUpdatedEvent(int orderLine) : base(orderLine)
    {

    }
}