using System;

namespace LibraRestaurant.Shared.Events.Role;

public sealed class RoleCreatedEvent : DomainEvent
{
    public RoleCreatedEvent(int roleId) : base(roleId)
    {
    }
}