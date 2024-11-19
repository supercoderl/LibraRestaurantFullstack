using System;
using LibraRestaurant.Domain.Enums;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;

public sealed class UpdateEmployeeCommand : CommandBase
{
    private static readonly UpdateEmployeeCommandValidation s_validation = new();

    public Guid EmployeeId { get; }
    public Guid? StoreId { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public UserStatus Status { get; }
    public string Mobile { get; }

    public UpdateEmployeeCommand(
        Guid employeeId,
        Guid? storeId,
        string email,
        string firstName,
        string lastName,
        UserStatus status,
        string mobile) : base(employeeId)
    {
        EmployeeId = employeeId;
        StoreId = storeId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Status = status;
        Mobile = mobile;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}