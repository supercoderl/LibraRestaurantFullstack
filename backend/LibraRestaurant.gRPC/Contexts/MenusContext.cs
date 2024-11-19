using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class MenusContext : IMenusContext
    {
        private readonly MenusApi.MenusApiClient _client;

        public MenusContext(MenusApi.MenusApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<MenuViewModel>> GetMenusByIds(IEnumerable<int> ids)
        {
            var request = new GetMenusByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Menus.Select(menu => new MenuViewModel(
                menu.Id,
                menu.Name,
                Guid.Parse(menu.StoreId),
                menu.Description,
                menu.IsActive,
                menu.IsDeleted));
        }
    }
}
