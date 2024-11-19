using System;

namespace LibraRestaurant.Shared.Menus;

public sealed record MenuViewModel(
    int MenuId,
    string Name,
    Guid StoreId,
    string? Description,
    bool IsActive,
    bool IsDeleted);