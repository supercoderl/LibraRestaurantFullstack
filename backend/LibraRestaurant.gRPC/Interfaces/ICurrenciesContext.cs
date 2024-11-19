using LibraRestaurant.Shared.Currencies;
using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface ICurrenciesContext
    {
        Task<IEnumerable<CurrencyViewModel>> GetCurrenciesByIds(IEnumerable<int> ids);
    }
}
