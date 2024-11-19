using LibraRestaurant.Application.ViewModels.MenuItems;
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
    public sealed class ItemViewModelSortProvider : ISortingExpressionProvider<ItemViewModel, MenuItem>
    {
        private static readonly Dictionary<string, Expression<Func<MenuItem, object>>> s_expressions = new()
        {
            { "title", item => item.Title },
            { "slug", item => item.Slug },
            { "summary", item => item.Summary ?? string.Empty },
            { "sku", item => item.SKU },
            { "price", item => item.Price },
            { "quantity", item => item.Quantity },
            { "recipe", item => item.Recipe ?? string.Empty },
            { "instruction", item => item.Instruction ?? string.Empty },
            { "createdAt", item => item.CreatedAt },
            { "lastUpdatedAt", item => item.LastUpdatedAt ?? DateTime.Now }
        };

        public Dictionary<string, Expression<Func<MenuItem, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
