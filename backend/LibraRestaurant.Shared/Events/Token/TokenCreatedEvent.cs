using System;

namespace LibraRestaurant.Shared.Events.Token;

public sealed class TokenCreatedEvent : DomainEvent
{
    public TokenCreatedEvent(int tokenId) : base(tokenId)
    {
    }
}