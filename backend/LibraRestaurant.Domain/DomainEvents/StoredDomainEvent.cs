using System;
using LibraRestaurant.Shared.Events;

namespace LibraRestaurant.Domain.DomainEvents;

public class StoredDomainEvent : DomainEvent
{
    public Guid Id { get; private set; }
    public string Data { get; private set; } = string.Empty;
    public string Employee { get; private set; } = string.Empty;
    public string CorrelationId { get; private set; } = string.Empty;

    public StoredDomainEvent(
        DomainEvent domainEvent,
        string data,
        string employee,
        string correlationId)
        : base(domainEvent.AggregateId, domainEvent.MessageType)
    {
        Id = Guid.NewGuid();
        Data = data;
        Employee = employee;
        CorrelationId = correlationId;
    }

    // EF Constructor
    protected StoredDomainEvent() : base(Guid.NewGuid())
    {
    }
}