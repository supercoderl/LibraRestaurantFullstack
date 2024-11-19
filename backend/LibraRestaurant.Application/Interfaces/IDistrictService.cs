using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Districts;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IDistrictService
    {
        public Task<DistrictViewModel?> GetDistrictByIdAsync(int districtId);

        public Task<PagedResult<DistrictViewModel>> GetAllDistrictsAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null);
    }
}
