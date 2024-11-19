using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.MenuItems;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IMenuItemService
    {
        public Task<ItemViewModel?> GetItemByIdAsync(int itemId);
        public Task<ItemViewModel?> GetItemBySlugAsync(string slug);

        public Task<PagedResult<ItemViewModel>> GetAllItemsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null,
            int categoryId = -1);

        public Task<int> CreateItemAsync(CreateItemViewModel item);
        public Task UpdateItemAsync(UpdateItemViewModel item);
        public Task DeleteItemAsync(int itemId);
    }
}
