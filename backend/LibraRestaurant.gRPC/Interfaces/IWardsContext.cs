using LibraRestaurant.Shared.Wards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IWardsContext
    {
        Task<IEnumerable<WardViewModel>> GetWardsByIds(IEnumerable<int> ids);
    }
}
