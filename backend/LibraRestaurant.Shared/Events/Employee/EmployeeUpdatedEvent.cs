using System;

namespace LibraRestaurant.Shared.Events.Employee;

public sealed class EmployeeUpdatedEvent : DomainEvent
{

    public EmployeeUpdatedEvent(Guid employeeId) : base(employeeId)
    {

    }
}