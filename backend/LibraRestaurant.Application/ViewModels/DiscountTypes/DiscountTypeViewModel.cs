using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.DiscountTypes;

public sealed class DiscountTypeViewModel
{
    public int DiscountTypeId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsPercentage { get; private set; }
    public double Value { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public string? CounponCode { get; private set; }
    public double MinOrderValue { get; private set; }
    public int MinItemQuantity { get; private set; }
    public double MaxDiscountValue { get; private set; }

    public static DiscountTypeViewModel FromDiscountType(DiscountType discountType)
    {
        return new DiscountTypeViewModel
        {
            DiscountTypeId = discountType.DiscountTypeId,
            Name = discountType.Name,
            Description = discountType.Description,
            IsPercentage = discountType.IsPercentage,
            Value = discountType.Value,
            CreatedAt = discountType.CreatedAt,
            StartTime = discountType.StartTime,
            EndTime = discountType.EndTime,
            CounponCode = discountType.CounponCode,
            MinOrderValue = discountType.MinOrderValue,
            MinItemQuantity = discountType.MinItemQuantity,
            MaxDiscountValue = discountType.MaxDiscountValue,
        };
    }
}