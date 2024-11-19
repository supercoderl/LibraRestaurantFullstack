using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IMenusContext
    {
        Task<IEnumerable<MenuViewModel>> GetMenusByIds(IEnumerable<int> ids);
    }
}
