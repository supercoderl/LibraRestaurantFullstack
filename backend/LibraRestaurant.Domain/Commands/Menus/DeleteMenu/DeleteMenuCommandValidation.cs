using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Menus.DeleteMenu
{
    public sealed class DeleteMenuCommandValidation : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.MenuId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Menu.EmptyId)
                .WithMessage("Menu id may not be empty");
        }
    }
}
