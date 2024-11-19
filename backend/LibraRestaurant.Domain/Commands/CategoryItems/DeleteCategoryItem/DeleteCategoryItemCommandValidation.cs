using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.DeleteCategoryItem
{
    public sealed class DeleteCategoryItemCommandValidation : AbstractValidator<DeleteCategoryItemCommand>
    {
        public DeleteCategoryItemCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.CategoryItemId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.CategoryItem.EmptyId)
                .WithMessage("Category item id may not be empty");
        }
    }
}
