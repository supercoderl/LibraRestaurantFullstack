using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class StoresApiImplementation : StoresApi.StoresApiBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoresApiImplementation(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public override async Task<GetStoresByIdsResult> GetByIds(
            GetStoresByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<Guid>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(Guid.Parse(id));
            }

            var stores = await _storeRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(store => idsAsIntegers.Contains(store.StoreId))
                .Select(store => new GrpcStore
                {
                    StoreId = store.StoreId.ToString(),
                    Name = store.Name,
                    CityId = store.CityId,
                    DistrictId = store.DistrictId,
                    WardId = store.WardId,
                    TaxCode = store.TaxCode,
                    Address = store.Address,
                    GpsLocation = store.GpsLocation,
                    PostalCode = store.PostalCode,
                    Phone = store.Phone,
                    Fax = store.Fax,
                    Email = store.Email,
                    Website = store.Website,
                    Logo = store.Logo,
                    BankBranch = store.BankBranch,
                    BankCode = store.BankCode,
                    BankAccount = store.BankAccount,
                    IsActive = store.IsActive,
                    IsDeleted = store.Deleted
                })
                .ToListAsync();

            var result = new GetStoresByIdsResult();

            result.Stores.AddRange(stores);

            return result;
        }
    }
}
