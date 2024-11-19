using LibraRestaurant.Application.ViewModels.Roles;
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
    public sealed class RoleViewModelSortProvider : ISortingExpressionProvider<RoleViewModel, Role>
    {
        private static readonly Dictionary<string, Expression<Func<Role, object>>> s_expressions = new()
    {
        { "name", menu => menu.Name },
        { "description", menu => menu.Description ?? string.Empty }
    };

        public Dictionary<string, Expression<Func<Role, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
