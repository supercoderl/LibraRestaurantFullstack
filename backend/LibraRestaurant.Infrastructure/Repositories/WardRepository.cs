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
    public sealed class WardRepository : BaseRepository<Ward>, IWardRepository
    {
        public WardRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Ward?> GetByDistrictAsync(int districtId)
        {
            return await DbSet.SingleOrDefaultAsync(ward => ward.DistrictId == districtId);
        }
    }
}
