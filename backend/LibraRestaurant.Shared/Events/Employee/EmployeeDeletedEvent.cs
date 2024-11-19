using System;

namespace LibraRestaurant.Shared.Events.Employee;

public sealed class EmployeeDeletedEvent : DomainEvent
{

    public EmployeeDeletedEvent(Guid employeeId) : base(employeeId)
    {

    }
}