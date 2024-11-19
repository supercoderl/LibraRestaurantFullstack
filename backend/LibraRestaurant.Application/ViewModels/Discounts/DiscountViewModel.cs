using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Discounts;

public sealed class DiscountViewModel
{
    public int DiscountId { get; private set; }
    public int DiscountTypeId { get; private set; }
    public int? CategoryId { get; private set; }
    public int? ItemId { get; private set; }
    public DiscountTargetType DiscountTargetType { get; private set; }
    public Guid? OrderId { get; private set; }
    public Guid? InvoiceId { get; private set; }
    public string? Comments { get; private set; }
    public string? DiscountTypeName { get; private set; }
    public string? CategoryName { get; private set; }
    public string? FoodName { get; private set; }

    public static DiscountViewModel FromDiscount(Discount discount)
    {
        return new DiscountViewModel
        {
            DiscountId = discount.DiscountId,
            DiscountTypeId = discount.DiscountTypeId,
            CategoryId = discount.CategoryId,
            ItemId = discount.ItemId,
            DiscountTargetType = discount.DiscountTargetType,
            OrderId = discount.OrderId,
            InvoiceId = discount.InvoiceId,
            Comments = discount.Comments,
            DiscountTypeName = discount.DiscountType?.Name,
            CategoryName = discount.Category?.Name,
            FoodName = discount.Item?.Title
        };
    }
}