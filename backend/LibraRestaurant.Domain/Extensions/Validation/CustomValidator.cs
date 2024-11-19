using System.Text.RegularExpressions;
using LibraRestaurant.Domain.Errors;
using FluentValidation;

namespace LibraRestaurant.Domain.Extensions.Validation;

public static partial class CustomValidator
{
    public static IRuleBuilderOptions<T, string> StringMustBeBase64<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(x => IsBase64String(x));
    }

    private static bool IsBase64String(string base64)
    {
        base64 = base64.Trim();
        return base64.Length % 4 == 0 && Base64Regex().IsMatch(base64);
    }

    public static IRuleBuilder<T, string> Password<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength = 8,
        int maxLength = 50)
    {
        var options = ruleBuilder
            .NotEmpty().WithErrorCode(DomainErrorCodes.Employee.EmptyPassword)
            .MinimumLength(minLength).WithErrorCode(DomainErrorCodes.Employee.ShortPassword)
            .MaximumLength(maxLength).WithErrorCode(DomainErrorCodes.Employee.LongPassword)
            .Matches("[A-Z]").WithErrorCode(DomainErrorCodes.Employee.UppercaseLetterPassword)
            .Matches("[a-z]").WithErrorCode(DomainErrorCodes.Employee.LowercaseLetterPassword)
            .Matches("[0-9]").WithErrorCode(DomainErrorCodes.Employee.NumberPassword)
            .Matches("[^a-zA-Z0-9]").WithErrorCode(DomainErrorCodes.Employee.SpecialCharPassword);
        return options;
    }

    [GeneratedRegex("^[a-zA-Z0-9\\+/]*={0,3}$", RegexOptions.None)]
    private static partial Regex Base64Regex();
}