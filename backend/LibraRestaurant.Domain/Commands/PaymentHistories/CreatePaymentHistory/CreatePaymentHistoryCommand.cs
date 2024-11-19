
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentHistories.CreatePaymentHistory
{
    public sealed class CreatePaymentHistoryCommand : CommandBase
    {
        private static readonly CreatePaymentHistoryCommandValidation s_validation = new();

        public int PaymentHistoryId { get; }
        public string TransactionId { get; }
        public Guid OrderId { get; }
        public int PaymentMethodId { get; }
        public double Amount { get; }
        public int? CurrencyId { get; }
        public PaymentStatus Status { get; }
        public string? ResponseJSON { get; }
        public string? CallbackURL { get; }
        public DateTime CreatedAt { get; }

        public CreatePaymentHistoryCommand(
            int paymentHistoryId,
            string transactionId,
            Guid orderId,
            int paymentMethodId,
            double amount,
            int? currencyId,
            string? responseJSON,
            string? callbackURL,
            DateTime createdAt,
            PaymentStatus status
        ) : base(paymentHistoryId)
        {
            PaymentHistoryId = paymentHistoryId;
            TransactionId = transactionId;
            PaymentMethodId = paymentMethodId;
            OrderId = orderId;
            Amount = amount;
            CurrencyId = currencyId;
            ResponseJSON = responseJSON;
            CallbackURL = callbackURL;
            CreatedAt = createdAt;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
