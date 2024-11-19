using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Menus;

public sealed record UpdateMenuViewModel(
    int MenuId,
    string Name,
    Guid StoreId,
    string? Description,
    bool IsActive);