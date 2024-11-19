using System;

namespace LibraRestaurant.Application.ViewModels.Menus;

public sealed record CreateMenuViewModel(
    string Name,
    Guid StoreId,
    string Description);