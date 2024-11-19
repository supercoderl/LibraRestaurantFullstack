using LibraRestaurant.Domain.Entities;
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
    public sealed class OrderRepository : BaseRepository<OrderHeader>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<OrderHeader?> GetByOrderNoAsync(string orderNo)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.OrderNo == orderNo);
        }

        public async Task<OrderHeader?> GetByStoreAndReservationAsync(Guid storeId, int reservationId)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.StoreId == storeId && item.ReservationId == reservationId && item.LatestStatus == Domain.Enums.OrderStatus.InPreperation);
        }

        public async Task<OrderHeader?> GetByStoreAsync(Guid storeId)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.StoreId == storeId);
        }

        public async Task<int> CountOrderAsync(int? month, int? year)
        {
            if(month is not null && year is not null)
            {
                return await DbSet.Where(o => o.LatestStatusUpdate.Month == month && o.LatestStatusUpdate.Year == year).CountAsync();
            }
            return await DbSet.CountAsync();
        }
    }
}
