using System;

namespace LibraRestaurant.Shared.DiscountTypes;

public sealed record DiscountTypeViewModel(
    int DiscountTypeId,
    string Name,
    string? Description,
    bool IsPercentage,
    double Value,
    DateTime CreatedAt,
    DateTime StartTime,
    DateTime? EndTime,
    string? CounponCode,
    double MinOrderValue,
    int MinItemQuantity,
    double MaxDiscountValue);