using System;

namespace LibraRestaurant.Shared.Events.District;

public sealed class DistrictDeletedEvent : DomainEvent
{

    public DistrictDeletedEvent(int districtId) : base(districtId)
    {

    }
}