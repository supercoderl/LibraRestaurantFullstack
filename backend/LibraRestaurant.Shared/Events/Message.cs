using System;
using MediatR;

namespace LibraRestaurant.Shared.Events;

public abstract class Message : IRequest
{
    public Guid AggregateId { get; private set; }
    public int AggregateNumberId { get; private set; }
    public string MessageType { get; protected set; }

    protected Message(Guid aggregateId)
    {
        AggregateId = aggregateId;
        MessageType = GetType().Name;
    }

    protected Message(Guid aggregateId, string? messageType)
    {
        AggregateId = aggregateId;
        MessageType = messageType ?? string.Empty;
    }

    protected Message(int aggregateNumberId)
    {
        AggregateNumberId = aggregateNumberId;
        MessageType = GetType().Name;
    }

    protected Message(int aggregateNumberId, string? messageType)
    {
        AggregateNumberId = aggregateNumberId;
        MessageType = messageType ?? string.Empty;
    }
}