using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.CategoryItems;

namespace LibraRestaurant.Application.Interfaces
{
    public interface ICategoryItemService
    {
        public Task<int> CreateCategoryItemAsync(CreateCategoryItemViewModel categoryItem);
        public Task UpdateCategoryItemAsync(UpdateCategoryItemViewModel categoryItem);
        public Task DeleteCategoryItemAsync(int categoryItemId);
    }
}
