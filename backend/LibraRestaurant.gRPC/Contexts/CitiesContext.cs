using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Cities;
using LibraRestaurant.Shared.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class CitiesContext : ICitiesContext
    {
        private readonly CitiesApi.CitiesApiClient _client;

        public CitiesContext(CitiesApi.CitiesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CityViewModel>> GetCitiesByIds(IEnumerable<int> ids)
        {
            var request = new GetCitiesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Cities.Select(city => new CityViewModel(
                city.CityId,
                city.Name,
                city.NameEn,
                city.Fullname,
                city.FullnameEn,
                city.CodeName,
                city.IsDeleted));
        }

    }
}
