using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Stores;

public sealed class StoreViewModel
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CityId { get; set; }
    public int DistrictId { get; set; }
    public int WardId { get; set; }
    public bool IsActive { get; set; }
    public string? TaxCode { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? GpsLocation { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Logo { get; set; }
    public string? BankBranch { get; set; }
    public string? BankCode { get; set; }
    public string? BankAccount { get; set; }

    public static StoreViewModel FromMenu(Store store)
    {
        return new StoreViewModel
        {
            StoreId = store.StoreId,
            Name = store.Name,
            CityId = store.CityId,
            DistrictId = store.DistrictId,
            WardId = store.WardId,
            IsActive = store.IsActive,
            TaxCode = store.TaxCode,
            Address = store.Address,
            GpsLocation = store.GpsLocation,
            PostalCode = store.PostalCode,
            Phone = store.Phone,
            Fax = store.Fax,
            Email = store.Email,
            Website = store.Website,
            Logo = store.Logo,
            BankBranch = store.BankBranch,
            BankCode = store.BankCode,
            BankAccount = store.BankAccount,
        };
    }
}