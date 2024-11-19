using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Infrastructure.Repositories
{
    public sealed class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Reservation?> GetByReservationIdAsync(int reservationId)
        {
            return await DbSet.SingleOrDefaultAsync(reservation => reservation.ReservationId == reservationId);
        }

        public async Task<Reservation?> GetByReservationTableNumberAndStoreIdAsync(int tableNumber, Guid storeId)
        {
            return await DbSet.SingleOrDefaultAsync(reservation => reservation.TableNumber == tableNumber && reservation.StoreId == storeId);
        }

        public async Task<List<Reservation>> GetByStatusAsync(ReservationStatus status)
        {
            try
            {
                return await DbSet.Where(reservation => reservation.Status == status).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
