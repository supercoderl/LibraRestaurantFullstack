using LibraRestaurant.Shared.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IRolesContext
    {
        Task<IEnumerable<RoleViewModel>> GetRolesByIds(IEnumerable<int> ids);
    }
}
