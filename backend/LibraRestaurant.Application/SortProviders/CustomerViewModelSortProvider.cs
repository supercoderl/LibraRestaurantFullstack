using LibraRestaurant.Application.ViewModels.Customers;
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
    public sealed class CustomerViewModelSortProvider : ISortingExpressionProvider<CustomerViewModel, Customer>
    {
        private static readonly Dictionary<string, Expression<Func<Customer, object>>> s_expressions = new()
    {
        { "name", customer => customer.Name },
        { "phone", customer => customer.Phone },
    };

        public Dictionary<string, Expression<Func<Customer, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
