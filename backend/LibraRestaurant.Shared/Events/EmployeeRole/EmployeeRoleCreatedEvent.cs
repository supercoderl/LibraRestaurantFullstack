using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.Events.EmployeeRole
{
    public sealed class EmployeeRoleCreatedEvent : DomainEvent
    {
        public EmployeeRoleCreatedEvent(int employeeRoleId) : base(employeeRoleId)
        {
        }
    }
}
