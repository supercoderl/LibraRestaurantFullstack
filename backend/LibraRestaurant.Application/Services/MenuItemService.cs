using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.Queries.MenuItems.GetAll;
using LibraRestaurant.Application.Queries.MenuItems.GetById;
using LibraRestaurant.Application.Queries.MenuItems.GetBySlug;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.MenuItems;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Commands.MenuItems.CreateItem;
using LibraRestaurant.Domain.Commands.MenuItems.DeleteItem;
using LibraRestaurant.Domain.Commands.MenuItems.UpdateItem;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Services
{
    public sealed class MenuItemService : IMenuItemService
    {
        private readonly IMediatorHandler _bus;
        private readonly IImageService _imageService;

        public MenuItemService(IMediatorHandler bus, IImageService imageService, ICategoryItemService categoryItemService)
        {
            _bus = bus;
            _imageService = imageService;
        }

        public async Task<int> CreateItemAsync(CreateItemViewModel item)
        {
            string? path = null;
            if(item.Base64 is not null)
            {
                path = await _imageService.UploadFile(item.Base64, string.Concat("Product-", DateTime.Now.Date.ToString("dd-MM-yyyy")), "Restaurant/Items");
            }

            await _bus.SendCommandAsync(new CreateItemCommand(
                0,
                item.Title,
                item.Slug,
                item.Summary,
                item.SKU,
                item.Price,
                item.Quantity,
                item.Recipe,
                item.Instruction,
                path,
                item.CategoryIds
            ));

            return 0;
        }

        public async Task DeleteItemAsync(int itemId)
        {
            await _bus.SendCommandAsync(new DeleteItemCommand(itemId));
        }

        public async Task<PagedResult<ItemViewModel>> GetAllItemsAsync(PageQuery query, bool includeDeleted, string searchTerm = "", SortQuery? sortQuery = null, int categoryId = -1)
        {
            return await _bus.QueryAsync(new GetAllItemsQuery(query, includeDeleted, searchTerm, sortQuery, categoryId));
        }

        public async Task<ItemViewModel?> GetItemByIdAsync(int itemId)
        {
            return await _bus.QueryAsync(new GetItemByIdQuery(itemId));
        }

        public async Task<ItemViewModel?> GetItemBySlugAsync(string slug)
        {
            return await _bus.QueryAsync(new GetItemBySlugQuery(slug));
        }

        public async Task UpdateItemAsync(UpdateItemViewModel item)
        {
            string? path = null;
            if (item.Base64 is not null)
            {
                path = await _imageService.UploadFile(item.Base64, string.Concat("Product-", DateTime.Now.Date.ToString("dd-MM-yyyy")), "Restaurant/Items");
            }

            await _bus.SendCommandAsync(new UpdateItemCommand(
                item.ItemId,
                item.Title,
                item.Slug,
                item.Summary,
                item.SKU,
                item.Price,
                item.Quantity,
                item.Recipe,
                item.Base64 is not null ? path : item.Picture,
                item.Instruction,
                item.CategoryIds
            )); 
        }
    }
}
