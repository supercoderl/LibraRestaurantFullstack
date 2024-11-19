using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.DeleteDiscountType
{
    public sealed class DeleteDiscountTypeCommandValidation : AbstractValidator<DeleteDiscountTypeCommand>
    {
        public DeleteDiscountTypeCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.DiscountTypeId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.DiscountType.EmptyId)
                .WithMessage("Discount type id may not be empty");
        }
    }
}
