using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.DiscountTypes;

public sealed record UpdateDiscountTypeViewModel(
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