using LibraRestaurant.Application.ViewModels.DiscountTypes;
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
    public sealed class DiscountTypeViewModelSortProvider : ISortingExpressionProvider<DiscountTypeViewModel, DiscountType>
    {
        private static readonly Dictionary<string, Expression<Func<DiscountType, object>>> s_expressions = new()
    {
        { "name", discountType => discountType.Name },
        { "description", discountType => discountType.Description ?? string.Empty }
    };

        public Dictionary<string, Expression<Func<DiscountType, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
