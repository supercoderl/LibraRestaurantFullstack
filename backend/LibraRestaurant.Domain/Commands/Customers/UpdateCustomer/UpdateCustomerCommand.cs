using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommand : CommandBase
    {
        private static readonly UpdateCustomerCommandValidation s_validation = new();

        public int CustomerId { get; }
        public string Name { get; }
        public string Phone { get; }
        public string? Email { get; }
        public string? Address { get; }

        public UpdateCustomerCommand(
            int customerId,
            string name,
            string phone,
            string? email,
            string? address) : base(customerId)
        {
            CustomerId = customerId;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
