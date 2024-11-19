using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Wards;

public sealed class WardViewModel
{
    public int WardId { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string Fullname { get; private set; } = string.Empty;
    public string FullnameEn { get; private set; } = string.Empty;
    public string CodeName { get; private set; } = string.Empty;
    public int DistrictId { get; private set; }

    public static WardViewModel FromWard(Ward ward)
    {
        return new WardViewModel
        {
            WardId = ward.WardId,
            Name = ward.Name,
            NameEn = ward.NameEn,
            Fullname = ward.Fullname,
            FullnameEn = ward.FullnameEn,
            CodeName = ward.CodeName,
            DistrictId = ward.DistrictId
        };
    }
}