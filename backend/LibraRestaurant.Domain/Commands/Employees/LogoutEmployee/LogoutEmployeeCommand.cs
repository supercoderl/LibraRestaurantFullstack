using System;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Employees.LogoutEmployee;

public sealed class LogoutEmployeeCommand : CommandBase,
    IRequest<string>
{
    private static readonly LogoutEmployeeCommandValidation s_validation = new();

    public string RefreshToken { get; set; }

    public LogoutEmployeeCommand(string refreshToken) : base(0)
    {
        RefreshToken = refreshToken;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}