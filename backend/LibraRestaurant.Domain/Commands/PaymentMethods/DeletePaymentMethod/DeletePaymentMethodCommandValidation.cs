using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentMethods.DeletePaymentMethod
{
    public sealed class DeletePaymentMethodCommandValidation : AbstractValidator<DeletePaymentMethodCommand>
    {
        public DeletePaymentMethodCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.PaymentMethodId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentMethod.EmptyId)
                .WithMessage("PaymentMethod id may not be empty");
        }
    }
}
