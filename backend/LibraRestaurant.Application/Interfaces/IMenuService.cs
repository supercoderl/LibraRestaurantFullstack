using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IMenuService
    {
        public Task<MenuViewModel?> GetMenuByIdAsync(int menuId);

        public Task<PagedResult<MenuViewModel>> GetAllMenusAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateMenuAsync(CreateMenuViewModel menu);
        public Task UpdateMenuAsync(UpdateMenuViewModel menu);
        public Task DeleteMenuAsync(int menuId);
    }
}
