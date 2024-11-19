using LibraRestaurant.Application.ViewModels.Orders;
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
    public sealed class OrderViewModelSortProvider : ISortingExpressionProvider<OrderViewModel, OrderHeader>
    {
        private static readonly Dictionary<string, Expression<Func<OrderHeader, object>>> s_expressions = new()
    {
        { "orderNo", order => order.OrderNo },
        { "storeId", order => order.StoreId },
        { "paymentMethodId", order => order.PaymentMethodId },
        { "paymentTimeId", order => order.PaymentTimeId },
        { "servantId", order => order.ServantId },
        { "cashierId", order => order.CashierId },
        { "customerNotes", order => order.CustomerNotes ?? string.Empty },
        { "reservationId", order => order.ReservationId },
        { "priceCalculated", order => order.PriceCalculated },
        { "priceAdjustment", order => order.PriceAdjustment ?? 0 },
        { "priceAdjustmentReason", order => order.PriceAdjustmentReason ?? string.Empty },
        { "subtotal", order => order.Subtotal },
        { "tax", order => order.Tax },
        { "total", order => order.Total },
        { "latestStatus", order => order.LatestStatus },
        { "latestStatusUpdate", order => order.LatestStatusUpdate },
        { "isPaid", order => order.IsPaid },
        { "isPreparationDelayed", order => order.IsPreparationDelayed },
        { "delayedTime", order => order.DelayedTime ?? DateTime.Now },
        { "isCancel", order => order.IsCanceled },
        { "canceledTime", order => order.CanceledTime ?? DateTime.Now },
        { "canceledReason", order => order.CanceledReason ?? string.Empty },
        { "isReady", order => order.IsReady },
        { "readyTime", order => order.ReadyTime ?? DateTime.Now },
        { "isCompleted", order => order.IsCompleted },
        { "completedTime", order => order.CompletedTime ?? DateTime.Now },
    };

        public Dictionary<string, Expression<Func<OrderHeader, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
