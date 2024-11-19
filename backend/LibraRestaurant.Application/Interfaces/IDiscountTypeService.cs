using LibraRestaurant.Application.ViewModels.EmployeeRoles;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.DiscountTypes;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IDiscountTypeService
    {
        public Task<DiscountTypeViewModel?> GetDiscountTypeByIdAsync(int discountTypeId);
        public Task<DiscountTypeViewModel?> GetDiscountTypeByCodeAsync(string counponCode);

        public Task<PagedResult<DiscountTypeViewModel>> GetAllDiscountTypesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateDiscountTypeAsync(CreateDiscountTypeViewModel discountType);
        public Task UpdateDiscountTypeAsync(UpdateDiscountTypeViewModel discountType);
        public Task DeleteDiscountTypeAsync(int discountTypeId);
    }
}
