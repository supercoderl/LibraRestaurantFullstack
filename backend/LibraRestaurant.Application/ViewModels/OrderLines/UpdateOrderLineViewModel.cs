using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.OrderLines;

public sealed record UpdateOrderLineViewModel(
    int OrderLineId,
    Guid OrderId,
    int ItemId,
    int Quantity,
    double FoodPrice,
    bool IsCanceled,
    DateTime? CanceledTime,
    string? CanceledReason,
    string? CustomerReview,
    CustomerLikeStatus CustomerLike);