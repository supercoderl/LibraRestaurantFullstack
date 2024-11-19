using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Categories.CreateCategory
{
    public sealed class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidation()
        {
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Category.EmptyName)
                .WithMessage("Name may not be empty");
        }
    }
}
