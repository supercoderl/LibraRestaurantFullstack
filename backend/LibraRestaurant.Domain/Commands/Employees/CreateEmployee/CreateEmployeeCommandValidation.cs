using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Extensions.Validation;
using FluentValidation;

namespace LibraRestaurant.Domain.Commands.Employees.CreateEmployee;

public sealed class CreateEmployeeCommandValidation : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidation()
    {
        AddRuleForId();
        AddRuleForEmail();
        AddRuleForFirstName();
        AddRuleForLastName();

        AddRuleForPassword();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.EmployeeId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Employee.EmptyId)
            .WithMessage("Employee id may not be empty");
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

    private void AddRuleForFirstName()
    {
        RuleFor(cmd => cmd.FirstName)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Employee.EmptyFirstName)
            .WithMessage("FirstName may not be empty")
            .MaximumLength(MaxLengths.Employee.FirstName)
            .WithErrorCode(DomainErrorCodes.Employee.FirstNameExceedsMaxLength)
            .WithMessage($"FirstName may not be longer than {MaxLengths.Employee.FirstName} characters");
    }

    private void AddRuleForLastName()
    {
        RuleFor(cmd => cmd.LastName)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Employee.EmptyLastName)
            .WithMessage("LastName may not be empty")
            .MaximumLength(MaxLengths.Employee.LastName)
            .WithErrorCode(DomainErrorCodes.Employee.LastNameExceedsMaxLength)
            .WithMessage($"LastName may not be longer than {MaxLengths.Employee.LastName} characters");
    }

    private void AddRuleForMobile()
    {
        RuleFor(cmd => cmd.Mobile)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Employee.EmptyMobile)
            .WithMessage("Mobile may not be empty")
            .MaximumLength(MaxLengths.Employee.Mobile)
            .WithErrorCode(DomainErrorCodes.Employee.MobileExceedsMaxLength)
            .WithMessage($"Mobile may not be longer than {MaxLengths.Employee.Mobile} characters");
    }

    private void AddRuleForPassword()
    {
        RuleFor(cmd => cmd.Password)
            .Password();
    }
}