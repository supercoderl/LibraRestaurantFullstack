using System;

namespace LibraRestaurant.Shared.Events.Currency;

public sealed class CurrencyCreatedEvent : DomainEvent
{
    public CurrencyCreatedEvent(int currencyId) : base(currencyId)
    {
    }
}