using System;

namespace LibraRestaurant.Shared.Events.Role;

public sealed class RoleDeletedEvent : DomainEvent
{

    public RoleDeletedEvent(int roleId) : base(roleId)
    {

    }
}