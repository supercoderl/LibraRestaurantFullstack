using LibraRestaurant.Domain.Commands.Categories.DeleteCurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Customers.DeleteCustomer
{
    public sealed class DeleteCustomerCommand : CommandBase
    {
        private static readonly DeleteCustomerCommandValidation s_validation = new();

        public int CustomerId { get; }

        public DeleteCustomerCommand(int customerId) : base(customerId)
        {
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
