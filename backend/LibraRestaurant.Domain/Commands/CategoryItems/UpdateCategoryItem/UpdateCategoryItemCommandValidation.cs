using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.UpdateCategoryItem
{
    public sealed class UpdateCategoryItemCommandValidation : AbstractValidator<UpdateCategoryItemCommand>
    {
        public UpdateCategoryItemCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.CategoryItemId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.CategoryItem.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}
