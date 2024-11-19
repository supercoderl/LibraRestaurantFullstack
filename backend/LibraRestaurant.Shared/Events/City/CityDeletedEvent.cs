using System;

namespace LibraRestaurant.Shared.Events.City;

public sealed class CityDeletedEvent : DomainEvent
{

    public CityDeletedEvent(int cityId) : base(cityId)
    {

    }
}