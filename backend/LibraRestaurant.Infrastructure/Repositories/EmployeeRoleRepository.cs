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
    public sealed class EmployeeRoleRepository : BaseRepository<EmployeeRole>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<EmployeeRole>> GetByEmployeeAsync(Guid employeeId)
        {
            return await DbSet.Where(employeeRole => employeeRole.EmployeeId == employeeId).ToListAsync();
        }
    }
}
