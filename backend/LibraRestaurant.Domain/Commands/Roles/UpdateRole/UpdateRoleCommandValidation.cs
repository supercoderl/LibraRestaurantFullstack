using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Roles.UpdateRole
{
    public sealed class UpdateRoleCommandValidation : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidation()
        {
            AddRuleForRoleId();
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Role.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForRoleId()
        {
            RuleFor(cmd => cmd.RoleId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Role.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}
