using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Wards;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IWardService
    {
        public Task<WardViewModel?> GetWardByIdAsync(int wardId);

        public Task<PagedResult<WardViewModel>> GetAllWardsAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null);
    }
}
