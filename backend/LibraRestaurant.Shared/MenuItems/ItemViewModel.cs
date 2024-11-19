using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.MenuItem
{
    public sealed record ItemViewModel(
        int ItemId,  
        string Title,
        string Slug,
        string? Summary,
        string SKU,
        double Price,
        int Quantity,
        string? Recipe,
        string? Instruction,
        string? Picture,
        bool IsDeleted
    );
}
