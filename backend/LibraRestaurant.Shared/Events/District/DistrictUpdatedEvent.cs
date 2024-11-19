using System;

namespace LibraRestaurant.Shared.Events.District;

public sealed class DistrictUpdatedEvent : DomainEvent
{

    public DistrictUpdatedEvent(int districtId) : base(districtId)
    {

    }
}