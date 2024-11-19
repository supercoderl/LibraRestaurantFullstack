using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Infrastructure.Repositories;

public sealed class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await DbSet.SingleOrDefaultAsync(employee => employee.Email == email);
    }
}