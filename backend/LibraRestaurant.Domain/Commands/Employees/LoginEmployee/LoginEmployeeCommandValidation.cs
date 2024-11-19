using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Extensions.Validation;
using FluentValidation;

namespace LibraRestaurant.Domain.Commands.Employees.LoginEmployee;

public sealed class LoginEmployeeCommandValidation : AbstractValidator<LoginEmployeeCommand>
{
    public LoginEmployeeCommandValidation()
    {
        AddRuleForEmail();
        AddRuleForPassword();
    }

    private void AddRuleForEmail()
    {
        RuleFor(cmd => cmd.Email)
            .EmailAddress()
            .WithErrorCode(DomainErrorCodes.Employee.InvalidEmail)
            .WithMessage("Email is not a valid email address")
            .MaximumLength(MaxLengths.Employee.Email)
            .WithErrorCode(DomainErrorCodes.Employee.EmailExceedsMaxLength)
            .WithMessage($"Email may not be longer than {MaxLengths.Employee.Email} characters");
    }

    private void AddRuleForPassword()
    {
        RuleFor(cmd => cmd.Password)
            .Password();
    }
}