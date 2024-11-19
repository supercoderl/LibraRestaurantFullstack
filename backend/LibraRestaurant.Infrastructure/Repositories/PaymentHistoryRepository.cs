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
    public sealed class PaymentHistoryRepository : BaseRepository<PaymentHistory>, IPaymentHistoryRepository
    {
        public PaymentHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PaymentHistory?> GetByOrderAsync(Guid orderId)
        {
            return await DbSet.SingleOrDefaultAsync(ph => ph.OrderId == orderId && ph.Status == Domain.Enums.PaymentStatus.Success);
        }

        public async Task<double> GetPaymentAmount()
        {
            return await DbSet
                .Where(ph => 
                    ph.Status == Domain.Enums.PaymentStatus.Success &&
                    ph.CreatedAt.Month == DateTime.Now.Month &&
                    ph.CreatedAt.Year == DateTime.Now.Year
                )
                .SumAsync(ph => ph.Amount);
        }
    }
}
