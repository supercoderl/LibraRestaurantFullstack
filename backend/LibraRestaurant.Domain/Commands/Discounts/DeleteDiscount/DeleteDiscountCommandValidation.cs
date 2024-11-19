using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Discounts.DeleteDiscount
{
    public sealed class DeleteDiscountCommandValidation : AbstractValidator<DeleteDiscountCommand>
    {
        public DeleteDiscountCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.DiscountId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Discount.EmptyId)
                .WithMessage("Discount id may not be empty");
        }
    }
}
