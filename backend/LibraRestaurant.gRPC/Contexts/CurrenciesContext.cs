using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Currencies;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Shared.Currencies;
using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class CurrenciesContext : ICurrenciesContext
    {
        private readonly CurrenciesApi.CurrenciesApiClient _client;

        public CurrenciesContext(CurrenciesApi.CurrenciesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CurrencyViewModel>> GetCurrenciesByIds(IEnumerable<int> ids)
        {
            var request = new GetCurrenciesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Currencies.Select(currency => new CurrencyViewModel(
                currency.Id,
                currency.Name,
                currency.Description,
                currency.IsDeleted));
        }
    }
}
