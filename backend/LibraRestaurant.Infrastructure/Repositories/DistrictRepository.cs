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
    public sealed class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<District?> GetByCityAsync(int cityId)
        {
            return await DbSet.SingleOrDefaultAsync(district => district.CityId == cityId);
        }
    }
}
