using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentMethods.CreatePaymentMethod
{
    public sealed class CreatePaymentMethodCommandValidation : AbstractValidator<CreatePaymentMethodCommand>
    {
        public CreatePaymentMethodCommandValidation()
        {
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentMethod.EmptyName)
                .WithMessage("Name may not be empty");
        }
    }
}
