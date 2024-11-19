using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandValidation : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidation()
        {
            AddRuleForName();
            AddRuleForPhone();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Customer.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForPhone()
        {
            RuleFor(cmd => cmd.Phone)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Customer.EmptyPhone)
                .WithMessage("Phone may not be empty");
        }
    }
}
