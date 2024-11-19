using System;

namespace LibraRestaurant.Shared.Wards;

public sealed record WardViewModel(
    int WardId,
    string Name,
    string NameEn,
    string Fullname,
    string FullnameEn,
    string CodeName,
    int DistrictId,
    bool IsDeleted);