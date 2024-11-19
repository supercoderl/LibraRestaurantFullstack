using System;

namespace LibraRestaurant.Shared.Events.Employee;

public sealed class EmployeeCreatedEvent : DomainEvent
{
    public EmployeeCreatedEvent(Guid employeeId) : base(employeeId)
    {
    }
}