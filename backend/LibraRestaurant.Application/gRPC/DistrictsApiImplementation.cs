using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Districts;
using LibraRestaurant.Proto.Menus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class DistrictsApiImplementation : DistrictsApi.DistrictsApiBase
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictsApiImplementation(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public override async Task<GetDistrictsByIdsResult> GetByIds(
            GetDistrictsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var districts = await _districtRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(district => idsAsIntegers.Contains(district.DistrictId))
                .Select(district => new GrpcDistrict
                {
                    DistrictId = district.DistrictId,
                    Name = district.Name,
                    NameEn = district.NameEn,
                    Fullname = district.Fullname,
                    FullnameEn = district.Fullname,
                    CodeName = district.CodeName,
                    CityId = district.CityId,
                    IsDeleted = district.Deleted
                })
                .ToListAsync();

            var result = new GetDistrictsByIdsResult();

            result.Districts.AddRange(districts);

            return result;
        }
    }
}
