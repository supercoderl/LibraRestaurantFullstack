using System;

namespace LibraRestaurant.Application.ViewModels.DiscountTypes;

public sealed record CreateDiscountTypeViewModel(
    string Name,
    string? Description,
    bool IsPercentage,
    double Value,
    DateTime StartTime,
    DateTime? EndTime,
    string? CounponCode,
    double MinOrderValue,
    int MinItemQuantity,
    double MaxDiscountValue);