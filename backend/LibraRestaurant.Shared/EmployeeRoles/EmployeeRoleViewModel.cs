using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.EmployeeRoles
{
    public sealed record EmployeeRoleViewModel(
        int EmployeeRoleId,
        int RoleId,
        Guid EmployeeId,
        DateTime AssigedDate,
        DateTime? RevokedDate);
}
