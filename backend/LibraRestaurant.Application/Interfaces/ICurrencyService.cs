using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Currencies;

namespace LibraRestaurant.Application.Interfaces
{
    public interface ICurrencyService
    {
        public Task<CurrencyViewModel?> GetCurrencyByIdAsync(int currencyId);

        public Task<PagedResult<CurrencyViewModel>> GetAllCurrenciesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateCurrencyAsync(CreateCurrencyViewModel currency);
        public Task UpdateCurrencyAsync(UpdateCurrencyViewModel currency);
        public Task DeleteCurrencyAsync(int currencyId);
    }
}
