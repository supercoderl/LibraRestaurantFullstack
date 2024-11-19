using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.EmployeeRoles;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IRoleService
    {
        public Task<RoleViewModel?> GetRoleByIdAsync(int roleId);

        public Task<PagedResult<RoleViewModel>> GetAllRolesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateRoleAsync(CreateRoleViewModel role);
        public Task UpdateRoleAsync(UpdateRoleViewModel role);
        public Task AssignRoleAsync(AssignRoleViewModel request);
        public Task DeleteRoleAsync(int roleId);
    }
}
