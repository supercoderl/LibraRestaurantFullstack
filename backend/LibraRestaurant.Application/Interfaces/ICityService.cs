﻿using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Cities;

namespace LibraRestaurant.Application.Interfaces
{
    public interface ICityService
    {
        public Task<CityViewModel?> GetCityByIdAsync(int cityId);

        public Task<PagedResult<CityViewModel>> GetAllCitiesAsync(
            PageQuery query,
            bool includeDeleted,
            bool isAll,
            string searchTerm = "",
            SortQuery? sortQuery = null);
    }
}