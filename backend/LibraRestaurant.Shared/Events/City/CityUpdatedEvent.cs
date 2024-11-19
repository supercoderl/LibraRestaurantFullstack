using System;

namespace LibraRestaurant.Shared.Events.City;

public sealed class CityUpdatedEvent : DomainEvent
{

    public CityUpdatedEvent(int cityId) : base(cityId)
    {

    }
}