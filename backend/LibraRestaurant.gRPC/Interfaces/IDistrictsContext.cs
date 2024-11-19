using LibraRestaurant.Shared.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IDistrictsContext
    {
        Task<IEnumerable<DistrictViewModel>> GetDistrictsByIds(IEnumerable<int> ids);
    }
}
