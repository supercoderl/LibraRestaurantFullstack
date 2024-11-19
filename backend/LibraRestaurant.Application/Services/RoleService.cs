using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.Queries.Roles.GetRoleById;
using LibraRestaurant.Application.Queries.Roles.GetAll;
using LibraRestaurant.Domain.Commands.Roles.CreateRole;
using LibraRestaurant.Domain.Commands.Roles.UpdateRole;
using LibraRestaurant.Domain.Commands.Roles.DeleteRole;
using LibraRestaurant.Application.ViewModels.EmployeeRoles;
using LibraRestaurant.Domain.Commands.EmployeeRoles.UpsertEmployeeRole;

namespace LibraRestaurant.Application.Services
{
    public sealed class RoleService : IRoleService
    {
        private readonly IMediatorHandler _bus;

        public RoleService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<RoleViewModel?> GetRoleByIdAsync(int roleId)
        {
            return await _bus.QueryAsync(new GetRoleByIdQuery(roleId));
        }

        public async Task<PagedResult<RoleViewModel>> GetAllRolesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllRolesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateRoleAsync(CreateRoleViewModel role)
        {
            await _bus.SendCommandAsync(new CreateRoleCommand(
                role.RoleId,
                role.Name,
                role.Description));

            return 0;
        }

        public async Task AssignRoleAsync(AssignRoleViewModel request)
        {
            await _bus.SendCommandAsync(new UpsertEmployeeRoleCommand(
                0,
                request.RoleIds,
                request.EmployeeId,
                DateTime.Now,
                null
            ));
        }

        public async Task UpdateRoleAsync(UpdateRoleViewModel role)
        {
            await _bus.SendCommandAsync(new UpdateRoleCommand(
                role.RoleId,
                role.Name,
                role.Description));
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            await _bus.SendCommandAsync(new DeleteRoleCommand(roleId));
        }
    }
}
