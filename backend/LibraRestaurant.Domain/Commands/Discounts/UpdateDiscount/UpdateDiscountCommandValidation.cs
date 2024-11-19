using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Discounts.UpdateDiscount
{
    public sealed class UpdateDiscountCommandValidation : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountCommandValidation()
        {
            AddRuleForRoleId();
        }

        private void AddRuleForRoleId()
        {
            RuleFor(cmd => cmd.DiscountId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Discount.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}
