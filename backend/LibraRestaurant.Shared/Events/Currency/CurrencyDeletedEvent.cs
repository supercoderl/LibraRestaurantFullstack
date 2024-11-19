using System;

namespace LibraRestaurant.Shared.Events.Currency;

public sealed class CurrencyDeletedEvent : DomainEvent
{

    public CurrencyDeletedEvent(int currencyId) : base(currencyId)
    {

    }
}