using LibraRestaurant.Shared.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IPaymentMethodsContext
    {
        Task<IEnumerable<PaymentMethodViewModel>> GetPaymentMethodsByIds(IEnumerable<int> ids);
    }
}
