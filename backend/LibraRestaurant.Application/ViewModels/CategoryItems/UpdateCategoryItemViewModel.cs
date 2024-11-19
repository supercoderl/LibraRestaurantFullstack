using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.CategoryItems;

public sealed record UpdateCategoryItemViewModel(
    int CategoryItemId,
    int CategoryId,
    int ItemId,
    string? Description);