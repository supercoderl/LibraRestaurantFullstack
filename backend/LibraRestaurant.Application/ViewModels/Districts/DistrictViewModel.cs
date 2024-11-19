using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Districts;

public sealed class DistrictViewModel
{
    public int DistrictId { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string Fullname { get; private set; } = string.Empty;
    public string FullnameEn { get; private set; } = string.Empty;
    public string CodeName { get; private set; } = string.Empty;
    public int CityId { get; private set; }

    public static DistrictViewModel FromDistrict(District district)
    {
        return new DistrictViewModel
        {
            DistrictId = district.DistrictId,
            Name = district.Name,
            NameEn = district.NameEn,
            Fullname = district.Fullname,
            FullnameEn = district.FullnameEn,
            CodeName = district.CodeName,
            CityId = district.CityId
        };
    }
}