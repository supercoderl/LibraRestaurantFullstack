using LibraRestaurant.Application.ViewModels.Categories;
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
    public sealed class CategoryViewModelSortProvider : ISortingExpressionProvider<CategoryViewModel, Category>
    {
        private static readonly Dictionary<string, Expression<Func<Category, object>>> s_expressions = new()
    {
        { "name", category => category.Name },
        { "description", category => category.Description ?? string.Empty },
        { "isActive", category => category.IsActive }
    };

        public Dictionary<string, Expression<Func<Category, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
