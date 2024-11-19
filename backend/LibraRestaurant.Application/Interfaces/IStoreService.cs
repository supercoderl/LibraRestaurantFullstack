using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Stores;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IStoreService
    {
        public Task<StoreViewModel?> GetStoreByIdAsync(Guid storeId);

        public Task<PagedResult<StoreViewModel>> GetAllStoresAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateStoreAsync(CreateStoreViewModel store);
        public Task UpdateStoreAsync(UpdateStoreViewModel store);
        public Task DeleteStoreAsync(Guid storeId);
    }
}
