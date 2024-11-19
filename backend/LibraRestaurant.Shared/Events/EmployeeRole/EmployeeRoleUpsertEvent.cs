using System;

namespace LibraRestaurant.Shared.Events.EmployeeRole;

public sealed class EmployeeRoleUpsertEvent : DomainEvent
{
    public EmployeeRoleUpsertEvent(int employeeRoleId) : base(employeeRoleId)
    {
    }
}