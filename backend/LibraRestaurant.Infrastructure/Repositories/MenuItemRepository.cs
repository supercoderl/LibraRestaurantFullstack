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
    public sealed class MenuItemRepository : BaseRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<MenuItem?> GetBySlugAsync(string slug)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.Slug == slug);
        }
    }
}
