using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Roles.CreateRole
{
    public sealed class CreateRoleCommandValidation : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidation()
        {
            AddRuleForName();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Role.EmptyName)
                .WithMessage("Name may not be empty");
        }
    }
}
