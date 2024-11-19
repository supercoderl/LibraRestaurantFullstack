using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Wards;
using LibraRestaurant.Application.Queries.Wards.GetWardById;
using LibraRestaurant.Application.Queries.Wards.GetAll;

namespace LibraRestaurant.Application.Services
{
    public sealed class WardService : IWardService
    {
        private readonly IMediatorHandler _bus;

        public WardService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<WardViewModel?> GetWardByIdAsync(int wardId)
        {
            return await _bus.QueryAsync(new GetWardByIdQuery(wardId));
        }

        public async Task<PagedResult<WardViewModel>> GetAllWardsAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllWardsQuery(query, includeDeleted, isAll, searchTerm, sortQuery));
        }
    }
}
