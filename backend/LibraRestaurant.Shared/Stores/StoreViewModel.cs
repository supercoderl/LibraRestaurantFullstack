using System;

namespace LibraRestaurant.Shared.Stores;

public sealed record StoreViewModel(
    Guid StoreId,
    string Name,
    int CityId,
    int DistrictId,
    int WardId,
    bool IsActive,
    string? TaxCode,
    string Address,
    string? GpsLocation,
    string? PostalCode,
    string? Phone,
    string? Fax,
    string? Email,
    string? Website,
    string? Logo,
    string? BankBranch,
    string? BankCode,
    string? BankAccount,
    bool IsDeleted);