using LibraRestaurant.Shared.CustomerLines;
using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IOrderLinesContext
    {
        Task<IEnumerable<OrderLineViewModel>> GetOrderLinesByIds(IEnumerable<int> ids);
    }
}
