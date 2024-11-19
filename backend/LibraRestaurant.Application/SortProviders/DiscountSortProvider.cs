using LibraRestaurant.Application.ViewModels.Discounts;
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
    public sealed class DiscountViewModelSortProvider : ISortingExpressionProvider<DiscountViewModel, Discount>
    {
        private static readonly Dictionary<string, Expression<Func<Discount, object>>> s_expressions = new()
    {

    };

        public Dictionary<string, Expression<Func<Discount, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
