using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Reservations
{
    public sealed record UpdateReservationCustomerViewModel(
        int ReservationId,
        ReservationStatus Status,
        string CustomerName,
        string CustomerPhone
    );
}
