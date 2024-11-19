using System;

namespace LibraRestaurant.Shared.CategoryItems;

public sealed record CategoryItemViewModel(
    int CategoryItemId,
    int CategoryId,
    int ItemId,
    string? Description);