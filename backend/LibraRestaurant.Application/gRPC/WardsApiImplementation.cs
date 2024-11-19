using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.Wards;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class WardsApiImplementation : WardsApi.WardsApiBase
    {
        private readonly IWardRepository _wardRepository;

        public WardsApiImplementation(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }

        public override async Task<GetWardsByIdsResult> GetByIds(
            GetWardsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var wards = await _wardRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(ward => idsAsIntegers.Contains(ward.WardId))
                .Select(ward => new GrpcWard
                {
                    WardId = ward.WardId,
                    Name = ward.Name,
                    NameEn = ward.NameEn,
                    Fullname = ward.Fullname,
                    FullnameEn = ward.Fullname,
                    CodeName = ward.CodeName,   
                    DistrictId = ward.DistrictId,
                    IsDeleted = ward.Deleted
                })
                .ToListAsync();

            var result = new GetWardsByIdsResult();

            result.Wards.AddRange(wards);

            return result;
        }
    }
}
