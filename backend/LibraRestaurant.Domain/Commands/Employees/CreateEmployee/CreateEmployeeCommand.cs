using System;

namespace LibraRestaurant.Domain.Commands.Employees.CreateEmployee;

public sealed class CreateEmployeeCommand : CommandBase
{
    private static readonly CreateEmployeeCommandValidation s_validation = new();

    public Guid EmployeeId { get; }
    public Guid? StoreId { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Mobile { get; }
    public string Password { get; }

    public CreateEmployeeCommand(
        Guid employeeId,
        Guid? storeId,
        string email,
        string firstName,
        string lastName,
        string mobile,
        string password) : base(employeeId)
    {
        EmployeeId = employeeId;
        StoreId = storeId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}