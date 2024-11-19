using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.Reviews
{
    public sealed record ReviewViewModel(
        int ReviewId,
        int ItemId,
        string CustomerName,
        string? CustomerEmail,
        int Rating,
        string Comment,
        string? Picture,
        int LikeCounts,
        bool IsVerifiedPurchase,
        string? Response
    );
}
