using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Cities;
using LibraRestaurant.Proto.Menus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class CitiesApiImplementation : CitiesApi.CitiesApiBase
    {
        private readonly ICityRepository _cityRepository;

        public CitiesApiImplementation(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public override async Task<GetCitiesByIdsResult> GetByIds(
            GetCitiesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var citys = await _cityRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(city => idsAsIntegers.Contains(city.CityId))
                .Select(city => new GrpcCity
                {
                    CityId = city.CityId,
                    Name = city.Name,
                    NameEn = city.NameEn,
                    Fullname = city.Fullname,
                    FullnameEn = city.Fullname,
                    CodeName = city.CodeName,
                    IsDeleted = city.Deleted
                })
                .ToListAsync();

            var result = new GetCitiesByIdsResult();

            result.Cities.AddRange(citys);

            return result;
        }
    }
}
