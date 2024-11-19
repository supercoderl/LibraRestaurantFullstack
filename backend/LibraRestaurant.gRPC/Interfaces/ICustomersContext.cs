using LibraRestaurant.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface ICustomersContext
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomersByIds(IEnumerable<int> ids);
    }
}
