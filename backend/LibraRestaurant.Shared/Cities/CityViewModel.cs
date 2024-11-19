using System;

namespace LibraRestaurant.Shared.Cities;

public sealed record CityViewModel(
    int CityId,
    string Name,
    string NameEn,
    string Fullname,
    string FullnameEn,
    string CodeName,
    bool IsDeleted);