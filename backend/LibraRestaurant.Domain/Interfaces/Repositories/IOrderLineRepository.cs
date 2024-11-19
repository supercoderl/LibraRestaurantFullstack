using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Interfaces.Repositories
{
    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        Task<OrderLine?> GetByOrderAndItemAsync(Guid orderId, int itemId);
    }
}
