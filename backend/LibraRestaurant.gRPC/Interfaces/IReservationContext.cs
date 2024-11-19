using LibraRestaurant.Shared.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IReservationsContext
    {
        Task<IEnumerable<ReservationViewModel>> GetReservationsByIds(IEnumerable<int> ids);
    }
}
