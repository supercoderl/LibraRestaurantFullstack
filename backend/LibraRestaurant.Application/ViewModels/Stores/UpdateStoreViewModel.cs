using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Stores;

public sealed record UpdateStoreViewModel(
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
    string? BankAccount);