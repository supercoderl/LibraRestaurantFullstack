using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibraRestaurant.Application.SortProviders
{
    public sealed class MenuViewModelSortProvider : ISortingExpressionProvider<MenuViewModel, Menu>
    {
        private static readonly Dictionary<string, Expression<Func<Menu, object>>> s_expressions = new()
    {
        { "name", menu => menu.Name },
        { "storeId", menu => menu.StoreId },
        { "description", menu => menu.Description ?? string.Empty },
        { "isActive", menu => menu.IsActive }
    };

        public Dictionary<string, Expression<Func<Menu, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
