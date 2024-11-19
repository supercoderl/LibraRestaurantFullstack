using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Enums
{
    public enum PaymentID
    {
       Unknown = 0,
       Paypal = 1,
       VNPay = 2,
       Stripe = 3,
       PayOS = 4
    }
}
