
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Discounts.DeleteDiscount
{
    public sealed class DeleteDiscountCommand : CommandBase
    {
        private static readonly DeleteDiscountCommandValidation s_validation = new();

        public int DiscountId { get; }

        public DeleteDiscountCommand(int discountId) : base(discountId)
        {
            DiscountId = discountId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
