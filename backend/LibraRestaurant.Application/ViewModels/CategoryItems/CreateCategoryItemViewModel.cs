using System;
using System.Collections.Generic;

namespace LibraRestaurant.Application.ViewModels.CategoryItems;

public sealed record CreateCategoryItemViewModel(
    int CategoryId,
    int ItemId,
    string? Description);