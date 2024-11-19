using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Categories;

namespace LibraRestaurant.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryViewModel?> GetCategoryByIdAsync(int categoryId);

        public Task<PagedResult<CategoryViewModel>> GetAllCategoriesAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateCategoryAsync(CreateCategoryViewModel category);
        public Task UpdateCategoryAsync(UpdateCategoryViewModel category);
        public Task DeleteCategoryAsync(int categoryId);
    }
}
