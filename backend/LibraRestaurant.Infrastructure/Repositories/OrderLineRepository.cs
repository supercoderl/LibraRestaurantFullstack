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
    public sealed class OrderLineRepository : BaseRepository<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<OrderLine?> GetByOrderAndItemAsync(Guid orderId, int itemId)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.OrderId == orderId && item.ItemId == itemId);
        }
    }
}
