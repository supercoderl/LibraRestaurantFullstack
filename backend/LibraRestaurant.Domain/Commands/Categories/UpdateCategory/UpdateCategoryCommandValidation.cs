using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Categories.UpdateCategory
{
    public sealed class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation()
        {
            AddRuleForMenuId();
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Category.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForMenuId()
        {
            RuleFor(cmd => cmd.CategoryId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Category.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}
