using System;

namespace LibraRestaurant.Shared.Events.OrderLine;

public sealed class OrderLineCreatedEvent : DomainEvent
{
    public OrderLineCreatedEvent(int orderLineId) : base(orderLineId)
    {
    }
}