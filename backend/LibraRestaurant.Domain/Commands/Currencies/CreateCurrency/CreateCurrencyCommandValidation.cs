using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Currencies.CreateCurrency
{
    public sealed class CreateCurrencyCommandValidation : AbstractValidator<CreateCurrencyCommand>
    {
        public CreateCurrencyCommandValidation()
        {
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Currency.EmptyName)
                .WithMessage("Name may not be empty");
        }
    }
}
