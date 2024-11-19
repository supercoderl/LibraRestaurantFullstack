using LibraRestaurant.Domain.Enums;
using System;

namespace LibraRestaurant.Application.ViewModels.Discounts;

public sealed record CreateDiscountViewModel(
    int DiscountTypeId,
    int? CategoryId,
    DiscountTargetType DiscountTargetType,
    Guid? OrderId,
    Guid? InvoiceId,
    int? ItemId,
    string? Comments);