using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class EmployeeRole : Entity
    {
        public int EmployeeRoleId { get; private set; }
        public int RoleId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public DateTime AssignedDate { get; private set; }
        public DateTime? RevokedDate { get; private set; }

        [ForeignKey("RoleId")]
        [InverseProperty("EmployeeRoles")]
        public virtual Role? Role { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeRoles")]
        public virtual Employee? Employee { get; set; }

        public EmployeeRole(
            int employeeRoleId,
            int roleId,
            Guid employeeId,
            DateTime assignedDate,
            DateTime? revokedDate
        ) : base(employeeRoleId)
        {
            EmployeeRoleId = employeeRoleId;
            RoleId = roleId;
            EmployeeId = employeeId;
            AssignedDate = assignedDate;
            RevokedDate = revokedDate;
        }

        public void SetRoleId( int roleId )
        {
            RoleId = roleId;
        }

        public void SetEmployeeId( Guid employeeId )
        {
            EmployeeId = employeeId;
        }

        public void SetAssignedDate( DateTime assignedDate )
        {
            AssignedDate = assignedDate;
        }

        public void SetRevokedDate( DateTime? revokedDate )
        {
            RevokedDate = revokedDate;
        }
    }
}
