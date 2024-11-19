using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Enums
{
    public enum ReservationStatus
    {
        Available,
        Reserved,
        Occupied,
        Cleaning,
        OutOfService
    }
}
