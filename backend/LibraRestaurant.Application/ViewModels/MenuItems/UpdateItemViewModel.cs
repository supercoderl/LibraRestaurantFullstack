using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.MenuItems
{
    public sealed record UpdateItemViewModel(
        int ItemId,
        string Title,
        string Slug,
        string? Summary,
        string SKU,
        double Price,
        int Quantity,
        string? Recipe,
        string? Instruction,
        string? Base64,
        string? Picture,
        List<int> CategoryIds
    );
}
