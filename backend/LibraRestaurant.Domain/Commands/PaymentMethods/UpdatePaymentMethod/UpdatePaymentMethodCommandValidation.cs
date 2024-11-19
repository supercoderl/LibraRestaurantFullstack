using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentMethods.UpdatePaymentMethod
{
    public sealed class UpdatePaymentMethodCommandValidation : AbstractValidator<UpdatePaymentMethodCommand>
    {
        public UpdatePaymentMethodCommandValidation()
        {
            AddRuleForPaymentMethodId();
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentMethod.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForPaymentMethodId()
        {
            RuleFor(cmd => cmd.PaymentMethodId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentMethod.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}
