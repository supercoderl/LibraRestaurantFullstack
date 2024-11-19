using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.Queries.Currencies.GetCurrencyById;
using LibraRestaurant.Application.Queries.Currencies.GetAll;
using LibraRestaurant.Domain.Commands.Currencies.CreateCurrency;
using LibraRestaurant.Domain.Commands.Currencies.UpdateCurrency;
using LibraRestaurant.Domain.Commands.Currencies.DeleteCurrency;

namespace LibraRestaurant.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IMediatorHandler _bus;

        public CurrencyService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<CurrencyViewModel?> GetCurrencyByIdAsync(int currencyId)
        {
            return await _bus.QueryAsync(new GetCurrencyByIdQuery(currencyId));
        }

        public async Task<PagedResult<CurrencyViewModel>> GetAllCurrenciesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCurrenciesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateCurrencyAsync(CreateCurrencyViewModel currency)
        {
            await _bus.SendCommandAsync(new CreateCurrencyCommand(
                0,
                currency.Name,
                currency.Description));

            return 0;
        }

        public async Task UpdateCurrencyAsync(UpdateCurrencyViewModel currency)
        {
            await _bus.SendCommandAsync(new UpdateCurrencyCommand(
                currency.CurrencyId,
                currency.Name,
                currency.Description));
        }

        public async Task DeleteCurrencyAsync(int currencyId)
        {
            await _bus.SendCommandAsync(new DeleteCurrencyCommand(currencyId));
        }
    }
}
