using System;

namespace LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;

public sealed class DeleteEmployeeCommand : CommandBase
{
    private static readonly DeleteEmployeeCommandValidation s_validation = new();

    public Guid EmployeeId { get; }

    public DeleteEmployeeCommand(Guid employeeId) : base(employeeId)
    {
        EmployeeId = employeeId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}