using LibraRestaurant.Shared.PaymentHistorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IPaymentHistoriesContext
    {
        Task<IEnumerable<PaymentHistoryViewModel>> GetPaymentHistoriesByIds(IEnumerable<int> ids);
    }
}
