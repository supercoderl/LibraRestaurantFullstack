using LibraRestaurant.Domain.Entities;
using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Stores;
using LibraRestaurant.Shared.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class StoresContext : IStoresContext
    {
        private readonly StoresApi.StoresApiClient _client;

        public StoresContext(StoresApi.StoresApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<StoreViewModel>> GetStoresByIds(IEnumerable<Guid> ids)
        {
            var request = new GetStoresByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Stores.Select(store => new StoreViewModel(
                Guid.Parse(store.StoreId),
                store.Name,
                store.CityId,
                store.DistrictId,
                store.WardId,
                store.IsActive,
                store.TaxCode,
                store.Address,
                store.GpsLocation,
                store.PostalCode,
                store.Phone,
                store.Fax,
                store.Email,
                store.Website,
                store.Logo,
                store.BankBranch,
                store.BankCode,
                store.BankAccount,
                store.IsDeleted));
        }
    }
}
