using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Tokens.CreateToken
{
    public sealed class CreateTokenCommandValidation : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidation()
        {
            AddRuleForName();
            AddRuleForStore();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.OldToken)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Token.EmptyOldToken)
                .WithMessage("Token may not be empty");
        }

        private void AddRuleForStore()
        {
            RuleFor(cmd => cmd.EmployeeId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Token.EmptyEmployee)
                .WithMessage("Employee may not be empty");
        }
    }
}
