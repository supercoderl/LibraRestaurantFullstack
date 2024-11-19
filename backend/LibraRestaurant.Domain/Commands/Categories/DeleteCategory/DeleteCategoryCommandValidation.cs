using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Categories.DeleteCategory
{
    public sealed class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.CategoryId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Category.EmptyId)
                .WithMessage("Category id may not be empty");
        }
    }
}
