using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Menus.CreateMenu
{
    public sealed class CreateMenuCommandValidation : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidation()
        {
            AddRuleForName();
            AddRuleForStore();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Menu.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForStore()
        {
            RuleFor(cmd => cmd.StoreId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Menu.EmptyStrore)
                .WithMessage("Store may not be empty");
        }
    }
}
