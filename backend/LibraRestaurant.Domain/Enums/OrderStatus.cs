using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Enums
{
    public enum OrderStatus
    {
        Draft,
        Confirmed,
        InPreperation,
        Ready,
        Completed,
        Canceled,
        Delayed,
        Paid,
        Failed,
        Refunded
    }
}
