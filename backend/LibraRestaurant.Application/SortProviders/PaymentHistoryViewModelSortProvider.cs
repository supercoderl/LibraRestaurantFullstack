using LibraRestaurant.Application.ViewModels.PaymentHistories;
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
    public sealed class PaymentHistoryViewModelSortProvider : ISortingExpressionProvider<PaymentHistoryViewModel, PaymentHistory>
    {
        private static readonly Dictionary<string, Expression<Func<PaymentHistory, object>>> s_expressions = new()
    {

    };

        public Dictionary<string, Expression<Func<PaymentHistory, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
