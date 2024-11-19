using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Roles;
using LibraRestaurant.Shared.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class RolesContext : IRolesContext
    {
        private readonly RolesApi.RolesApiClient _client;

        public RolesContext(RolesApi.RolesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<RoleViewModel>> GetRolesByIds(IEnumerable<int> ids)
        {
            var request = new GetRolesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Roles.Select(role => new RoleViewModel(
                role.Id,
                role.Name,
                role.Description));
        }
    }
}
