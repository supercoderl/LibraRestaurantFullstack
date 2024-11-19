using LibraRestaurant.Shared.OrderHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IOrdersContext
    {
        Task<IEnumerable<OrderHeaderViewModel>> GetOrdersByIds(IEnumerable<Guid> ids);
    }
}
