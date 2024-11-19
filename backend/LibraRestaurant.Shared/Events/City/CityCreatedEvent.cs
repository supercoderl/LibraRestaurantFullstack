using System;

namespace LibraRestaurant.Shared.Events.City;

public sealed class CityCreatedEvent : DomainEvent
{
    public CityCreatedEvent(int cityId) : base(cityId)
    {
    }
}