using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Extensions.Validation;
using FluentValidation;

namespace LibraRestaurant.Domain.Commands.Employees.LogoutEmployee;

public sealed class LogoutEmployeeCommandValidation : AbstractValidator<LogoutEmployeeCommand>
{
    public LogoutEmployeeCommandValidation()
    {
        AddRuleForToken();
    }

    private void AddRuleForToken()
    {
        RuleFor(cmd => cmd.RefreshToken)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Token.EmptyOldToken)
            .WithMessage($"Token may not be empty");
    }
}