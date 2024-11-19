using LibraRestaurant.Application.ViewModels.EmployeeRoles;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Discounts;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IDiscountService
    {
        public Task<DiscountViewModel?> GetDiscountByIdAsync(int discountId);

        public Task<PagedResult<DiscountViewModel>> GetAllDiscountsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateDiscountAsync(CreateDiscountViewModel discount);
        public Task UpdateDiscountAsync(UpdateDiscountViewModel discount);
        public Task DeleteDiscountAsync(int discountId);
    }
}
