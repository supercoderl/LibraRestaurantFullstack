using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Roles.DeleteRole
{
    public sealed class DeleteRoleCommandValidation : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.RoleId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Role.EmptyId)
                .WithMessage("Role id may not be empty");
        }
    }
}
