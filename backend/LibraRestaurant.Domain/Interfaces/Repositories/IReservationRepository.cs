using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Interfaces.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation?> GetByReservationIdAsync(int reservationId);
        Task<Reservation?> GetByReservationTableNumberAndStoreIdAsync(int tableNumber, Guid storeId);
        Task<List<Reservation>> GetByStatusAsync(ReservationStatus status);
    }
}
