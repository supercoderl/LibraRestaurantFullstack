using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Categories;

public sealed record UpdateCategoryViewModel(
    int CategoryId,
    string Name,
    string? Description,
    bool IsActive,
    string? Picture,
    string? Base64);