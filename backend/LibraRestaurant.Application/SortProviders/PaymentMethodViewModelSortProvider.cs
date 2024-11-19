using LibraRestaurant.Application.ViewModels.PaymentMethods;
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
    public sealed class PaymentMethodViewModelSortProvider : ISortingExpressionProvider<PaymentMethodViewModel, PaymentMethod>
    {
        private static readonly Dictionary<string, Expression<Func<PaymentMethod, object>>> s_expressions = new()
    {
        { "name", paymentMethod => paymentMethod.Name },
        { "description", paymentMethod => paymentMethod.Description ?? string.Empty },
        { "picture", paymentMethod => paymentMethod.Picture ?? string.Empty },
        { "isActive", paymentMethod => paymentMethod.IsActive }
    };

        public Dictionary<string, Expression<Func<PaymentMethod, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
