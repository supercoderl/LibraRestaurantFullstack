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
    public sealed class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Menu?> GetByStoreAsync(Guid storeId)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.StoreId == storeId);
        }
    }
}
