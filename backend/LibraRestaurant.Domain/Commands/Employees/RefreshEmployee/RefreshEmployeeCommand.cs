using System;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Employees.RefreshEmployee;

public sealed class RefreshEmployeeCommand : CommandBase,
    IRequest<Object>
{
    private static readonly RefreshEmployeeCommandValidation s_validation = new();

    public string RefreshToken { get; set; }

    public RefreshEmployeeCommand(string refreshToken) : base(0)
    {
        RefreshToken = refreshToken;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}