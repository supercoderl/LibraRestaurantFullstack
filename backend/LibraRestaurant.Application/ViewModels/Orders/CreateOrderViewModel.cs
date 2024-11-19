using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LibraRestaurant.Application.ViewModels.Orders;

public sealed record CreateOrderViewModel(
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
        OrderStatus LatestStatus,
        DateTime LatestStatusUpdate,
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
        List<CreateOrderLineViewModel> OrderLines);