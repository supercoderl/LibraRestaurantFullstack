using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentHistories.CreatePaymentHistory
{
    public sealed class CreatePaymentHistoryCommandValidation : AbstractValidator<CreatePaymentHistoryCommand>
    {
        public CreatePaymentHistoryCommandValidation()
        {
            AddRuleForTransaction();
            AddRuleForOrder();
            AddRuleForPaymentMethod();
            AddRuleForAmount();
        }

        private void AddRuleForTransaction()
        {
            RuleFor(cmd => cmd.TransactionId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentHistory.EmptyTransaction)
                .WithMessage("Transaction may not be empty");
        }

        private void AddRuleForOrder()
        {
            RuleFor(cmd => cmd.OrderId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentHistory.EmptyOrder)
                .WithMessage("Order may not be empty");
        }

        private void AddRuleForPaymentMethod()
        {
            RuleFor(cmd => cmd.PaymentMethodId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentHistory.EmptyPaymentMethod)
                .WithMessage("Payment method may not be empty");
        }

        private void AddRuleForAmount()
        {
            RuleFor(cmd => cmd.Amount)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PaymentHistory.EmptyAmount)
                .WithMessage("Amount may not be empty");
        }
    }
}
