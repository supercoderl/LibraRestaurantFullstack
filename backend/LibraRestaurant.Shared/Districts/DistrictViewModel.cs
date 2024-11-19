using System;

namespace LibraRestaurant.Shared.Districts;

public sealed record DistrictViewModel(
    int DistrictId,
    string Name,
    string NameEn,
    string Fullname,
    string FullnameEn,
    string CodeName,
    int CityId,
    bool IsDeleted);