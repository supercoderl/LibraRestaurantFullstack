using LibraRestaurant.Domain.Errors;
using FluentValidation;

namespace LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;

public sealed class DeleteEmployeeCommandValidation : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.EmployeeId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Employee.EmptyId)
            .WithMessage("Employee id may not be empty");
    }
}