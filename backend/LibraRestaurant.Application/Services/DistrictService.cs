using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.Queries.Districts.GetDistrictById;
using LibraRestaurant.Application.Queries.Districts.GetAll;

namespace LibraRestaurant.Application.Services
{
    public sealed class DistrictService : IDistrictService
    {
        private readonly IMediatorHandler _bus;

        public DistrictService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<DistrictViewModel?> GetDistrictByIdAsync(int districtId)
        {
            return await _bus.QueryAsync(new GetDistrictByIdQuery(districtId));
        }

        public async Task<PagedResult<DistrictViewModel>> GetAllDistrictsAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllDistrictsQuery(query, includeDeleted, isAll, searchTerm, sortQuery));
        }
    }
}
