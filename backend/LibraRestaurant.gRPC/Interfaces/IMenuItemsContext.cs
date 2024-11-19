using LibraRestaurant.Shared.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IMenuItemsContext
    {
        Task<IEnumerable<ItemViewModel>> GetItemsByIds(IEnumerable<int> ids);
    }
}
