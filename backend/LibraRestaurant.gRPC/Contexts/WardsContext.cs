using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Wards;
using LibraRestaurant.Shared.Wards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class WardsContext : IWardsContext
    {
        private readonly WardsApi.WardsApiClient _client;

        public WardsContext(WardsApi.WardsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<WardViewModel>> GetWardsByIds(IEnumerable<int> ids)
        {
            var request = new GetWardsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Wards.Select(ward => new WardViewModel(
                ward.WardId,
                ward.Name,
                ward.NameEn,
                ward.Fullname,
                ward.FullnameEn,
                ward.CodeName,
                ward.DistrictId,
                ward.IsDeleted));
        }

    }
}
