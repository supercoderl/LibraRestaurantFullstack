using System;
using MediatR;

namespace LibraRestaurant.Shared.Events;

public abstract class DomainEvent : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected DomainEvent(Guid aggregateId) : base(aggregateId)
    {
        Timestamp = DateTime.Now;
    }

    protected DomainEvent(int aggregateNumberId) : base(aggregateNumberId)
    {
        Timestamp = DateTime.Now;
    }

    protected DomainEvent(Guid aggregateId, string? messageType) : base(aggregateId, messageType)
    {
        Timestamp = DateTime.Now;
    }

    protected DomainEvent(int aggregateNumberId, string? messageType) : base(aggregateNumberId, messageType)
    {
        Timestamp = DateTime.Now;
    }
}