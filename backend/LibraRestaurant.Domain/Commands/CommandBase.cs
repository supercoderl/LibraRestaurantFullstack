using System;
using FluentValidation.Results;
using MediatR;

namespace LibraRestaurant.Domain.Commands;

public abstract class CommandBase : IRequest
{
    public Guid AggregateId { get; }
    public int AggregateNumberId { get; }
    public string MessageType { get; }
    public DateTime Timestamp { get; }
    public ValidationResult? ValidationResult { get; protected set; }

    protected CommandBase(Guid aggregateId)
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.Now;
        AggregateId = aggregateId;
    }

    protected CommandBase(int aggregateNumberId)
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.Now;
        AggregateNumberId = aggregateNumberId;
    }

    public abstract bool IsValid();
}