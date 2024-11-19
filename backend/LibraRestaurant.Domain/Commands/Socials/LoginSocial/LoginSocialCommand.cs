using System;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Socials.LoginSocial;

public sealed class LoginSocialCommand : CommandBase,
    IRequest<Object>
{
    private static readonly LoginSocialCommandValidation s_validation = new();

    public string Email { get; }


    public LoginSocialCommand(
        string email) : base(Guid.NewGuid())
    {
        Email = email;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}