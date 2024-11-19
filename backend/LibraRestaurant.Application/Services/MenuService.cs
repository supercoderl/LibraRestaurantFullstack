using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.Queries.Menus.GetAll;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Menus.CreateMenu;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Commands.Menus.DeleteMenu;
using LibraRestaurant.Application.Queries.Menus.GetMenuById;

namespace LibraRestaurant.Application.Services
{
    public sealed class MenuService : IMenuService
    {
        private readonly IMediatorHandler _bus;

        public MenuService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<MenuViewModel?> GetMenuByIdAsync(int menuId)
        {
            return await _bus.QueryAsync(new GetMenuByIdQuery(menuId));
        }

        public async Task<PagedResult<MenuViewModel>> GetAllMenusAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllMenusQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateMenuAsync(CreateMenuViewModel menu)
        {
            await _bus.SendCommandAsync(new CreateMenuCommand(
                0,
                menu.StoreId,
                menu.Name,
                menu.Description,
                true));

            return 0;
        }

        public async Task UpdateMenuAsync(UpdateMenuViewModel menu)
        {
            await _bus.SendCommandAsync(new UpdateMenuCommand(
                menu.MenuId,
                menu.Name,
                menu.StoreId,
                menu.Description,
                menu.IsActive));
        }

        public async Task DeleteMenuAsync(int menuId)
        {
            await _bus.SendCommandAsync(new DeleteMenuCommand(menuId));
        }
    }
}
