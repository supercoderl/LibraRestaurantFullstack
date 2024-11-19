using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandValidation : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            AddRuleForMenuId();
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Customer.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForMenuId()
        {
            RuleFor(cmd => cmd.CustomerId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Customer.EmptyId)
                .WithMessage("Id may not be empty");
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
