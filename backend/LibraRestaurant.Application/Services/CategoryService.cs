using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.Queries.Categories.GetCategoryById;
using LibraRestaurant.Application.Queries.Categories.GetAll;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Domain.Commands.Categories.CreateCategory;
using LibraRestaurant.Domain.Commands.Categories.UpdateCategory;
using LibraRestaurant.Domain.Commands.Categories.DeleteCategory;

namespace LibraRestaurant.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMediatorHandler _bus;
        private readonly IImageService _imageService;

        public CategoryService(IMediatorHandler bus, IImageService imageService)
        {
            _bus = bus;
            _imageService = imageService;
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int categoryId)
        {
            return await _bus.QueryAsync(new GetCategoryByIdQuery(categoryId));
        }

        public async Task<PagedResult<CategoryViewModel>> GetAllCategoriesAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCategoriesQuery(query, includeDeleted, isAll, searchTerm, sortQuery));
        }

        public async Task<int> CreateCategoryAsync(CreateCategoryViewModel category)
        {
            string? path = null;
            if (category.Base64 is not null)
            {
                path = await _imageService.UploadFile(category.Base64, string.Concat("Category-", DateTime.Now.Date.ToString("dd-MM-yyyy")), "Restaurant/Categories");
            }

            await _bus.SendCommandAsync(new CreateCategoryCommand(
                0,
                category.Name,
                category.Description,
                true,
                path));

            return 0;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryViewModel category)
        {
            string? path = null;
            if (category.Base64 is not null)
            {
                path = await _imageService.UploadFile(category.Base64, string.Concat("Category-", DateTime.Now.Date.ToString("dd-MM-yyyy")), "Restaurant/Categories");
            }

            await _bus.SendCommandAsync(new UpdateCategoryCommand(
                category.CategoryId,
                category.Name,
                category.Description,
                category.IsActive,
                category.Base64 is not null ? path : category.Picture));
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _bus.SendCommandAsync(new DeleteCategoryCommand(categoryId));
        }
    }
}
