using FluentValidation;
using LibraRestaurant.Domain.Commands.Currencies.DeleteCurrency;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Customers.DeleteCustomer
{
    public sealed class DeleteCustomerCommandValidation : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.CustomerId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Customer.EmptyId)
                .WithMessage("Customer id may not be empty");
        }
    }
}
