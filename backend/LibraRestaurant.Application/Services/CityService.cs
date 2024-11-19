using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.Queries.Wards.GetAll;
using LibraRestaurant.Application.Queries.Wards.GetWardById;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.Queries.Cities.GetCityById;
using LibraRestaurant.Application.Queries.Cities.GetAll;

namespace LibraRestaurant.Application.Services
{
    public sealed class CityService : ICityService
    {
        private readonly IMediatorHandler _bus;

        public CityService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<CityViewModel?> GetCityByIdAsync(int wardId)
        {
            return await _bus.QueryAsync(new GetCityByIdQuery(wardId));
        }

        public async Task<PagedResult<CityViewModel>> GetAllCitiesAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCitiesQuery(query, includeDeleted, isAll, searchTerm, sortQuery));
        }
    }
}
