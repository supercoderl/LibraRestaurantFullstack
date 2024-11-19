
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentHistories.DeletePaymentHistory
{
    public sealed class DeletePaymentHistoryCommand : CommandBase
    {
        private static readonly DeletePaymentHistoryCommandValidation s_validation = new();

        public int PaymentHistoryId { get; }

        public DeletePaymentHistoryCommand(int paymentHistoryId) : base(paymentHistoryId)
        {
            PaymentHistoryId = paymentHistoryId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
