using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Domain.Interfaces.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByEmailAsync(string email);
}