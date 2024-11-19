using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.SortProviders
{
    public sealed class CurrencyViewModelSortProvider : ISortingExpressionProvider<CurrencyViewModel, Currency>
    {
        private static readonly Dictionary<string, Expression<Func<Currency, object>>> s_expressions = new()
    {
        { "name", currency => currency.Name },
        { "description", currency => currency.Description ?? string.Empty },
    };

        public Dictionary<string, Expression<Func<Currency, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
