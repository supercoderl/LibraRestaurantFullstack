using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Interfaces.Repositories
{
    public interface IPaymentHistoryRepository : IRepository<PaymentHistory>
    {
        Task<PaymentHistory?> GetByOrderAsync(Guid orderId);
        Task<double> GetPaymentAmount();
    }
}
