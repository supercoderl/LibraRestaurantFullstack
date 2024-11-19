using System;

namespace LibraRestaurant.Shared.Events.OrderLine;

public sealed class OrderLineDeletedEvent : DomainEvent
{

    public OrderLineDeletedEvent(int orderLineId) : base(orderLineId)
    {

    }
}