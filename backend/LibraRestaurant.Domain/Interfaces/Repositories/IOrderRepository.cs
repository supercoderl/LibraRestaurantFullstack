using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<OrderHeader>
    {
        Task<OrderHeader?> GetByStoreAsync(Guid storeId);
        Task<OrderHeader?> GetByOrderNoAsync(string orderNo);
        Task<OrderHeader?> GetByStoreAndReservationAsync(Guid storeId, int reservationId);
        Task<int> CountOrderAsync(int? month, int? year);
    }
}
