using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class RolesApiImplementation : RolesApi.RolesApiBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesApiImplementation(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public override async Task<GetRolesByIdsResult> GetByIds(
            GetRolesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var roles = await _roleRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(role => idsAsIntegers.Contains(role.RoleId))
                .Select(role => new GrpcRole
                {
                    Id = role.RoleId,
                    Name = role.Name,
                    Description = role.Description
                })
                .ToListAsync();

            var result = new GetRolesByIdsResult();

            result.Roles.AddRange(roles);

            return result;
        }
    }
}
