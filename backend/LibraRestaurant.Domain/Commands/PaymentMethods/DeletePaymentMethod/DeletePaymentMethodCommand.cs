
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.PaymentMethods.DeletePaymentMethod
{
    public sealed class DeletePaymentMethodCommand : CommandBase
    {
        private static readonly DeletePaymentMethodCommandValidation s_validation = new();

        public int PaymentMethodId { get; }

        public DeletePaymentMethodCommand(int paymentMethodId) : base(paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
