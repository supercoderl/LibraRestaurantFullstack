using System;
using System.Collections;
using System.Collections.Generic;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Cities;

public sealed class CityViewModel
{
    public int CityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string FullnameEn { get; set; } = string.Empty;
    public string CodeName { get; set; } = string.Empty;

    public static CityViewModel FromCity(City city)
    {
        return new CityViewModel
        {
            CityId = city.CityId,
            Name = city.Name,
            NameEn = city.NameEn,
            Fullname = city.Fullname,
            FullnameEn = city.FullnameEn,
            CodeName = city.CodeName,
        };
    }
}