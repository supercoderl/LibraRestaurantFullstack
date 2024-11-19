
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.DeleteDiscountType
{
    public sealed class DeleteDiscountTypeCommand : CommandBase
    {
        private static readonly DeleteDiscountTypeCommandValidation s_validation = new();

        public int DiscountTypeId { get; }

        public DeleteDiscountTypeCommand(int discountTypeId) : base(discountTypeId)
        {
            DiscountTypeId = discountTypeId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
