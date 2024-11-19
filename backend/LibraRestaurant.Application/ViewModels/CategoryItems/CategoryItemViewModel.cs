using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.CategoryItems;

public sealed class CategoryItemViewModel
{
    public int CategoryId { get; set; }
    public int ItemId { get; set; }
    public string? Description { get; set; }

    public static CategoryItemViewModel FromCategoryItem(CategoryItem categoryItem)
    {
        return new CategoryItemViewModel
        {
            CategoryId = categoryItem.CategoryId,
            ItemId = categoryItem.ItemId,
            Description = categoryItem.Description,
        };
    }
}