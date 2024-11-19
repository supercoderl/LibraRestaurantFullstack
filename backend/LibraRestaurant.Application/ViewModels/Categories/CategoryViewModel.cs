using System;
using System.Collections.Generic;
using System.Linq;
using LibraRestaurant.Application.ViewModels.MenuItems;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Categories;

public sealed class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive {  get; set; } = true;
    public string? Picture {  get; set; }
    public int ItemNumber { get; set; }

    public static CategoryViewModel FromCategory(Category category)
    {
        return new CategoryViewModel
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive,
            Picture = category.Picture,
            ItemNumber = category.CategoryItems?.Count ?? 0
        };
    }
}