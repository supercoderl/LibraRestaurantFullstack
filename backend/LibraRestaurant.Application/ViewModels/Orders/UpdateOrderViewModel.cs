using System;
using System.Collections.Generic;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Menus;

public sealed record UpdateOrderViewModel(
    Guid OrderId,
    string? OrderNo,
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
    List<CreateOrderLineViewModel> OrderLines,
    string Action);