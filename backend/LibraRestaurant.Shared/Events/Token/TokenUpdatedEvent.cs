using System;

namespace LibraRestaurant.Shared.Events.Token;

public sealed class TokenUpdatedEvent : DomainEvent
{

    public TokenUpdatedEvent(int tokenId) : base(tokenId)
    {

    }
}