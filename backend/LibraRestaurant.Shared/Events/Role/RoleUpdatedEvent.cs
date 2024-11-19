using System;

namespace LibraRestaurant.Shared.Events.Role;

public sealed class RoleUpdatedEvent : DomainEvent
{

    public RoleUpdatedEvent(int roleId) : base(roleId)
    {

    }
}