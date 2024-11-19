using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Menu.UpdateMenu
{
    public sealed class UpdateMenuCommandValidation : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidation()
        {
            AddRuleForMenuId();
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

        private void AddRuleForMenuId()
        {
            RuleFor(cmd => cmd.MenuId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Menu.EmptyId)
                .WithMessage("Id may not be empty");
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
