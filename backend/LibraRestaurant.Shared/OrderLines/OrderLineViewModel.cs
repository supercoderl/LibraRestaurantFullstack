using System;

namespace LibraRestaurant.Shared.CustomerLines;

public sealed record OrderLineViewModel(
    int OrderLineId,
    Guid OrderId,
    int ItemId,
    int Quantity,
    double FoodPrice,
    bool IsCanceled,
    DateTime? CanceledTime,
    string? CanceledReason,
    string? CustomerReview,
    bool IsDeleted);