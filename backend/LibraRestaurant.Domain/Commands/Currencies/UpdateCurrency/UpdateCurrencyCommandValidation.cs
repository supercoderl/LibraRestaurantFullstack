using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Currencies.UpdateCurrency
{
    public sealed class UpdateCurrencyCommandValidation : AbstractValidator<UpdateCurrencyCommand>
    {
        public UpdateCurrencyCommandValidation()
        {
            AddRuleForMenuId();
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Currency.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForMenuId()
        {
            RuleFor(cmd => cmd.CurrencyId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Currency.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}
