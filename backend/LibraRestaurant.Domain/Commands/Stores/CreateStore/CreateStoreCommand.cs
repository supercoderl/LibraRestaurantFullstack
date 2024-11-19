
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Stores.CreateStore
{
    public sealed class CreateStoreCommand : CommandBase
    {
        private static readonly CreateStoreCommandValidation s_validation = new();

        public Guid StoreId { get; }
        public string Name { get; }
        public int CityId { get; }
        public int DistrictId { get; }
        public int WardId { get; }
        public bool IsActive { get; }
        public string? TaxCode { get; }
        public string Address { get; }
        public string? GpsLocation { get; }
        public string? PostalCode { get; }
        public string? Phone { get; }
        public string? Fax { get; }
        public string? Email { get; }
        public string? Website { get; }
        public string? Logo { get; }
        public string? BankBranch { get; }
        public string? BankCode { get; }
        public string? BankAccount { get; }

        public CreateStoreCommand(
            Guid storeId,
            string name,
            int cityId,
            int districtId,
            int wardId,
            bool isActive,
            string? taxCode,
            string address,
            string? gpsLocation,
            string? postalCode,
            string? phone,
            string? fax,
            string? email,
            string? website,
            string? logo,
            string? bankBranch,
            string? bankCode,
            string? bankAccount
        ) : base(storeId)
        {
            StoreId = storeId;
            Name = name;
            CityId = cityId;
            DistrictId = districtId;
            WardId = wardId;
            IsActive = isActive;
            TaxCode = taxCode;
            Address = address;
            GpsLocation = gpsLocation;
            PostalCode = postalCode;
            Phone = phone;
            Fax = fax;
            Email = email;
            Website = website;
            Logo = logo;
            BankBranch = bankBranch;
            BankCode = bankCode;
            BankAccount = bankAccount;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
