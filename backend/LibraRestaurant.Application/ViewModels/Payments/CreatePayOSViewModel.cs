using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public sealed record CreatePayOSViewModel(
        string ProductName,
        string Description,
        int Price,
        Guid OrderId,
        int PaymentMethodId,
        Guid TransactionId
    );
}
