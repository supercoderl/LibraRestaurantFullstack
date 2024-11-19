using System;

namespace LibraRestaurant.Shared.Discounts;

public sealed record DiscountViewModel(
    int DiscountId,
    int DiscountTypeId,
    int? CategoryId,
    Guid? OrderId,
    Guid? InvoiceId,
    int? ItemId,
    string? Comments);