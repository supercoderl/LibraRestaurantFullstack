using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public sealed record SessionStripe
    (
        long Amount,
        string Currency,
        string Name,
        string? Description,
        int Quantity,
        string Mode,
        string? CustomerEmail,
        Guid TransactionId,
        Guid OrderId,
        int PaymentMethodId
    );
}
