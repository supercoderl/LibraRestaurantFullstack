using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Stores.UpdateStore
{
    public sealed class UpdateStoreCommand : CommandBase
    {
        private static readonly UpdateStoreCommandValidation s_validation = new();

        public Guid StoreId { get; private set; }
        public string Name { get; private set; }
        public int CityId { get; private set; }
        public int DistrictId { get; private set; }
        public int WardId { get; private set; }
        public bool IsActive { get; private set; }
        public string? TaxCode { get; private set; }
        public string Address { get; private set; }
        public string? GpsLocation { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Phone { get; private set; }
        public string? Fax { get; private set; }
        public string? Email { get; private set; }
        public string? Website { get; private set; }
        public string? Logo { get; private set; }
        public string? BankBranch { get; private set; }
        public string? BankCode { get; private set; }
        public string? BankAccount { get; private set; }

        public UpdateStoreCommand(
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
            string? bankAccount) : base(storeId)
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
