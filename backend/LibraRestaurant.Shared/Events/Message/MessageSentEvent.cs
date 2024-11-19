using System;

namespace LibraRestaurant.Shared.Events.Messages;

public sealed class MessageSentEvent : DomainEvent
{
    public MessageSentEvent(int messageId) : base(messageId)
    {
    }
}