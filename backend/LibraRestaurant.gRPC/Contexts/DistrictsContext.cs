using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Districts;
using LibraRestaurant.Shared.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class DistrictsContext : IDistrictsContext
    {
        private readonly DistrictsApi.DistrictsApiClient _client;

        public DistrictsContext(DistrictsApi.DistrictsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<DistrictViewModel>> GetDistrictsByIds(IEnumerable<int> ids)
        {
            var request = new GetDistrictsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Districts.Select(district => new DistrictViewModel(
                district.DistrictId,
                district.Name,
                district.NameEn,
                district.Fullname,
                district.FullnameEn,
                district.CodeName,
                district.CityId,
                district.IsDeleted));
        }

    }
}
