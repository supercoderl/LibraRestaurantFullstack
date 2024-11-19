using System.Collections.Generic;
using System.Linq;
using LibraRestaurant.Domain.Commands.Employees.LoginEmployee;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.LoginUser;

public sealed class LoginUserCommandValidationTests :
    ValidationTestBase<LoginEmployeeCommand, LoginEmployeeCommandValidation>
{
    public LoginUserCommandValidationTests() : base(new LoginEmployeeCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Email()
    {
        var command = CreateTestCommand(string.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.InvalidEmail,
            "Email is not a valid email address");
    }

    [Fact]
    public void Should_Be_Invalid_For_Invalid_Email()
    {
        var command = CreateTestCommand("not a email");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.InvalidEmail,
            "Email is not a valid email address");
    }

    [Fact]
    public void Should_Be_Invalid_For_Email_Exceeds_Max_Length()
    {
        var command = CreateTestCommand(new string('a', MaxLengths.Employee.Email) + "@test.com");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.EmailExceedsMaxLength,
            $"Email may not be longer than {MaxLengths.Employee.Email} characters");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Password()
    {
        var command = CreateTestCommand(password: "");

        var errors = new List<string>
        {
            DomainErrorCodes.Employee.EmptyPassword,
            DomainErrorCodes.Employee.SpecialCharPassword,
            DomainErrorCodes.Employee.NumberPassword,
            DomainErrorCodes.Employee.LowercaseLetterPassword,
            DomainErrorCodes.Employee.UppercaseLetterPassword,
            DomainErrorCodes.Employee.ShortPassword
        };

        ShouldHaveExpectedErrors(command, errors.ToArray());
    }

    [Fact]
    public void Should_Be_Invalid_For_Missing_Special_Character()
    {
        var command = CreateTestCommand(password: "z8tnayvd5FNLU9AQm");

        ShouldHaveSingleError(command, DomainErrorCodes.Employee.SpecialCharPassword);
    }

    [Fact]
    public void Should_Be_Invalid_For_Missing_Number()
    {
        var command = CreateTestCommand(password: "z]tnayvdFNLU:]AQm");

        ShouldHaveSingleError(command, DomainErrorCodes.Employee.NumberPassword);
    }

    [Fact]
    public void Should_Be_Invalid_For_Missing_Lowercase_Character()
    {
        var command = CreateTestCommand(password: "Z8]TNAYVDFNLU:]AQM");

        ShouldHaveSingleError(command, DomainErrorCodes.Employee.LowercaseLetterPassword);
    }

    [Fact]
    public void Should_Be_Invalid_For_Missing_Uppercase_Character()
    {
        var command = CreateTestCommand(password: "z8]tnayvd5fnlu9:]aqm");

        ShouldHaveSingleError(command, DomainErrorCodes.Employee.UppercaseLetterPassword);
    }

    [Fact]
    public void Should_Be_Invalid_For_Password_Too_Short()
    {
        var command = CreateTestCommand(password: "zA6{");

        ShouldHaveSingleError(command, DomainErrorCodes.Employee.ShortPassword);
    }

    [Fact]
    public void Should_Be_Invalid_For_Password_Too_Long()
    {
        var command = CreateTestCommand(password: string.Concat(Enumerable.Repeat("zA6{", 12), 12));

        ShouldHaveSingleError(command, DomainErrorCodes.Employee.LongPassword);
    }

    private static LoginEmployeeCommand CreateTestCommand(
        string? email = null,
        string? password = null)
    {
        return new LoginEmployeeCommand(
            email ?? "test@email.com",
            password ?? "Po=PF]PC6t.?8?ks)A6W");
    }
}