using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentHistories.DeletePaymentHistory
{
    public sealed class DeletePaymentHistoryCommandValidation : AbstractValidator<DeletePaymentHistoryCommand>
    {
        public DeletePaymentHistoryCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.PaymentHistoryId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentHistory.EmptyId)
                .WithMessage("Payment history id may not be empty");
        }
    }
}
