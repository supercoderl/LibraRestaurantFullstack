using FluentValidation;
using LibraRestaurant.Domain.Commands.Currencies.DeleteCurrency;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Categories.DeleteCurrency
{
    public sealed class DeleteCurrencyCommandValidation : AbstractValidator<DeleteCurrencyCommand>
    {
        public DeleteCurrencyCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.CurrencyId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Currency.EmptyId)
                .WithMessage("Currency id may not be empty");
        }
    }
}
