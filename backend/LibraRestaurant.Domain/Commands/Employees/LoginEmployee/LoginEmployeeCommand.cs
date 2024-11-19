using System;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Employees.LoginEmployee;

public sealed class LoginEmployeeCommand : CommandBase,
    IRequest<Object>
{
    private static readonly LoginEmployeeCommandValidation s_validation = new();

    public string Email { get; set; }
    public string Password { get; set; }


    public LoginEmployeeCommand(
        string email,
        string password) : base(Guid.NewGuid())
    {
        Email = email;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}