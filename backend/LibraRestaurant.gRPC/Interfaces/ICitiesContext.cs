using LibraRestaurant.Shared.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface ICitiesContext
    {
        Task<IEnumerable<CityViewModel>> GetCitiesByIds(IEnumerable<int> ids);
    }
}
