using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Tokens.UpdateToken
{
    public sealed class UpdateTokenCommandValidation : AbstractValidator<UpdateTokenCommand>
    {
        public UpdateTokenCommandValidation()
        {
            AddRuleForEmployee();
            AddRuleForOldToken();
            AddRuleForTokenId();
        }

        private void AddRuleForOldToken()
        {
            RuleFor(cmd => cmd.OldToken)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Token.EmptyOldToken)
                .WithMessage("Token may not be empty");
        }

        private void AddRuleForTokenId()
        {
            RuleFor(cmd => cmd.TokenId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Token.EmptyId)
                .WithMessage("Id may not be empty");
        }

        private void AddRuleForEmployee()
        {
            RuleFor(cmd => cmd.EmployeeId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Token.EmptyEmployee)
                .WithMessage("Employee may not be empty");
        }
    }
}
