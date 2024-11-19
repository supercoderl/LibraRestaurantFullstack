using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class MenusApiImplementation : MenusApi.MenusApiBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenusApiImplementation(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public override async Task<GetMenusByIdsResult> GetByIds(
            GetMenusByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var menus = await _menuRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(menu => idsAsIntegers.Contains(menu.MenuId))
                .Select(menu => new GrpcMenu
                {
                    Id = menu.MenuId,
                    Name = menu.Name,
                    StoreId = menu.StoreId.ToString(),
                    Description = menu.Description,
                    IsActive = menu.IsActive,
                    IsDeleted = menu.Deleted
                })
                .ToListAsync();

            var result = new GetMenusByIdsResult();

            result.Menus.AddRange(menus);

            return result;
        }
    }
}
