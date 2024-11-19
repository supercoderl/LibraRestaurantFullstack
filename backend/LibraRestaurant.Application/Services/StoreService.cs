using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Stores;
using LibraRestaurant.Domain.Commands.Stores.CreateStore;
using LibraRestaurant.Domain.Commands.Stores.UpdateStore;
using LibraRestaurant.Domain.Commands.Stores.DeleteStore;
using LibraRestaurant.Application.Queries.Stores.GetAll;
using LibraRestaurant.Application.Queries.Stores.GetStoreById;

namespace LibraRestaurant.Application.Services
{
    public sealed class StoreService : IStoreService
    {
        private readonly IMediatorHandler _bus;

        public StoreService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<StoreViewModel?> GetStoreByIdAsync(Guid storeId)
        {
            return await _bus.QueryAsync(new GetStoreByIdQuery(storeId));
        }

        public async Task<PagedResult<StoreViewModel>> GetAllStoresAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllStoresQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateStoreAsync(CreateStoreViewModel store)
        {
            var storeId = Guid.NewGuid();

            await _bus.SendCommandAsync(new CreateStoreCommand(
                storeId,
                store.Name,
                store.CityId,
                store.DistrictId,
                store.WardId,
                true,
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
                store.BankAccount));

            return 0;
        }

        public async Task UpdateStoreAsync(UpdateStoreViewModel store)
        {
            await _bus.SendCommandAsync(new UpdateStoreCommand(
                store.StoreId,
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
                store.BankAccount));
        }

        public async Task DeleteStoreAsync(Guid storeId)
        {
            await _bus.SendCommandAsync(new DeleteStoreCommand(storeId));
        }
    }
}
