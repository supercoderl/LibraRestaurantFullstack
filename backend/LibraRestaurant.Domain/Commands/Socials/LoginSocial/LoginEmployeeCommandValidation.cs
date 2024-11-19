using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Extensions.Validation;
using FluentValidation;

namespace LibraRestaurant.Domain.Commands.Socials.LoginSocial;

public sealed class LoginSocialCommandValidation : AbstractValidator<LoginSocialCommand>
{
    public LoginSocialCommandValidation()
    {
        AddRuleForEmail();
    }

    private void AddRuleForEmail()
    {
        RuleFor(cmd => cmd.Email)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Google.EmptyEmail)
            .WithMessage("Email may not be empty");
    }
}