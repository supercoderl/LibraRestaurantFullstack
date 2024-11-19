using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Menus;

public sealed class MenuViewModel
{
    public int MenuId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StoreId { get; set; } = Guid.NewGuid();
    public string? Description { get; set; }
    public bool IsActive {  get; set; } = true;
    public string? StoreName { get; set; }

    public static MenuViewModel FromMenu(Menu menu)
    {
        return new MenuViewModel
        {
            MenuId = menu.MenuId,
            Name = menu.Name,
            StoreId = menu.StoreId,
            Description = menu.Description,
            IsActive = menu.IsActive,
            StoreName = menu?.Store?.Name
        };
    }
}