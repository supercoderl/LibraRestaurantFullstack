
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommand : CommandBase, IRequest<int>
    {
        private static readonly CreateCustomerCommandValidation s_validation = new();

        public int CustomerId { get; }
        public string Name { get; }
        public string Phone {  get; }
        public string? Email { get; }
        public string? Address { get; }

        public CreateCustomerCommand(
            int customerId,
            string name,
            string phone,
            string? email,
            string? address
        ) : base(customerId)
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
