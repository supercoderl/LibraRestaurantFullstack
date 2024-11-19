using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.EmployeeRoles
{
    public sealed record AssignRoleViewModel(
        Guid EmployeeId,
        List<int> RoleIds
    );
}
