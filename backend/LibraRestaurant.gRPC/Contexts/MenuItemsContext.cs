using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.MenuItems;
using LibraRestaurant.Shared.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class MenuItemsContext : IMenuItemsContext
    {
        private readonly ItemsApi.ItemsApiClient _client;

        public MenuItemsContext(ItemsApi.ItemsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ItemViewModel>> GetItemsByIds(IEnumerable<int> ids)
        {
            var request = new GetItemsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Items.Select(item => new ItemViewModel(
                item.Id,
                item.Title,
                item.Slug,
                item.Summary,
                item.Sku,
                item.Price,
                item.Quantity,
                item.Recipe,
                item.Instruction,
                item.Picture,
                item.IsDeleted));
        }

    }
}
