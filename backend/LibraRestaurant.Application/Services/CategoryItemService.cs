using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.CategoryItems;
using LibraRestaurant.Domain.Commands.CategoryItems.CreateCategoryItem;
using LibraRestaurant.Domain.Commands.CategoryItems.UpdateCategoryItem;
using LibraRestaurant.Domain.Commands.CategoryItems.DeleteCategoryItem;

namespace LibraRestaurant.Application.Services
{
    public class CategoryItemService : ICategoryItemService
    {
        private readonly IMediatorHandler _bus;

        public CategoryItemService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<int> CreateCategoryItemAsync(CreateCategoryItemViewModel categoryItem)
        {
            await _bus.SendCommandAsync(new CreateCategoryItemCommand(
                0,
                categoryItem.CategoryId,
                categoryItem.ItemId,
                categoryItem.Description));

            return 0;
        }

        public async Task UpdateCategoryItemAsync(UpdateCategoryItemViewModel categoryItem)
        {
            await _bus.SendCommandAsync(new UpdateCategoryItemCommand(
                categoryItem.CategoryItemId,
                categoryItem.CategoryId,
                categoryItem.ItemId,
                categoryItem.Description));
        }

        public async Task DeleteCategoryItemAsync(int categoryItemId)
        {
            await _bus.SendCommandAsync(new DeleteCategoryItemCommand(categoryItemId));
        }
    }
}
