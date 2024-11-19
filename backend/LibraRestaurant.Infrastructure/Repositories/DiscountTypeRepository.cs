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
    public sealed class DiscountTypeRepository : BaseRepository<DiscountType>, IDiscountTypeRepository
    {
        public DiscountTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<DiscountType?> GetByCodeAsync(string counponCode)
        {
            return await DbSet.SingleOrDefaultAsync(discountType => discountType.CounponCode == counponCode);
        }
    }
}
