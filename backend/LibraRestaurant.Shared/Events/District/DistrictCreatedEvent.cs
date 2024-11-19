using System;

namespace LibraRestaurant.Shared.Events.District;

public sealed class DistrictCreatedEvent : DomainEvent
{
    public DistrictCreatedEvent(int districtId) : base(districtId)
    {
    }
}