using System;

namespace LibraRestaurant.Shared.Events.Employee;

public sealed class PasswordChangedEvent : DomainEvent
{
    public PasswordChangedEvent(Guid employeeId) : base(employeeId)
    {
    }
}