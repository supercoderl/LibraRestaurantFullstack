using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.OrderHeaders
{
    public sealed record OrderHeaderViewModel(
        Guid OrderId,
        string OrderNo,
        Guid StoreId,
        int? PaymentMethodId,
        int? PaymentTimeId,
        Guid? ServantId,
        Guid? CashierId,
        string? CustomerNotes,
        int ReservationId,
        double PriceCalculated,
        double? PriceAdjustment,
        string? PriceAdjustmentReason,
        double Subtotal,
        double Tax,
        double Total,
        int? CustomerId,
        bool IsPaid,
        bool IsPreparationDelayed,
        DateTime? DelayedTime,
        bool IsCanceled,
        DateTime? CaceledTime,
        string? CanceledReason,
        bool IsReady,
        DateTime? ReadyTime,
        bool IsCompleted,
        DateTime? CompletedTime,
        bool IsDeleted);
}
