using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Discounts;

public sealed record UpdateDiscountViewModel(
    int DiscountId,
    int DiscountTypeId,
    int? CategoryId,
    int? ItemId,
    DiscountTargetType DiscountTargetType,
    Guid? OrderId,
    Guid? InvoiceId,
    string? Comments);