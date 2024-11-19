using LibraRestaurant.Application.ViewModels.OrderLines;
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
    public sealed class OrderLineViewModelSortProvider : ISortingExpressionProvider<OrderLineViewModel, OrderLine>
    {
        private static readonly Dictionary<string, Expression<Func<OrderLine, object>>> s_expressions = new()
    {
        { "orderId", orderLine => orderLine.OrderId },
        { "itemId", orderLine => orderLine.ItemId },
        { "quantity", orderLine => orderLine.Quantity },
        { "isCanceled", orderLine => orderLine.IsCanceled },
        { "canceledTime", orderLine => orderLine.CanceledTime ?? DateTime.Now },
        { "canceledReason", orderLine => orderLine.CanceledReason ?? string.Empty },
        { "customerReview", orderLine => orderLine.CustomerReview ?? string.Empty },
        { "customerLike", orderLine => orderLine.CustomerLike }
    };

        public Dictionary<string, Expression<Func<OrderLine, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
