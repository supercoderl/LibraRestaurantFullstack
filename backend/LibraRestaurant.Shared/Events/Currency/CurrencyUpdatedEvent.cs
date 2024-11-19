using System;

namespace LibraRestaurant.Shared.Events.Currency;

public sealed class CurrencyUpdatedEvent : DomainEvent
{

    public CurrencyUpdatedEvent(int currencyId) : base(currencyId)
    {

    }
}