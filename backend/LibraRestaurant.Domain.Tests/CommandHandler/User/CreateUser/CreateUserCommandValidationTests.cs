using System;
using System.Collections.Generic;
using System.Linq;
using LibraRestaurant.Domain.Commands.Employees.CreateEmployee;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.CreateUser;

public sealed class CreateItemCommandValidationTests :
    ValidationTestBase<CreateEmployeeCommand, CreateEmployeeCommandValidation>
{
    public CreateItemCommandValidationTests() : base(new CreateEmployeeCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_User_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.EmptyId,
            "User id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Email()
    {
        var command = CreateTestCommand(email: string.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.InvalidEmail,
            "Email is not a valid email address");
    }

    [Fact]
    public void Should_Be_Invalid_For_Invalid_Email()
    {
        var command = CreateTestCommand(email: "not a email");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.InvalidEmail,
            "Email is not a valid email address");
    }

    [Fact]
    public void Should_Be_Invalid_For_Email_Exceeds_Max_Length()
    {
        var command = CreateTestCommand(email: new string('a', MaxLengths.Employee.Email) + "@test.com");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.EmailExceedsMaxLength,
            $"Email may not be longer than {MaxLengths.Employee.Email} characters");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_First_Name()
    {
        var command = CreateTestCommand(firstName: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.EmptyFirstName,
            "FirstName may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_First_Name_Exceeds_Max_Length()
    {
        var command = CreateTestCommand(firstName: new string('a', MaxLengths.Employee.FirstName + 1));

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.FirstNameExceedsMaxLength,
            $"FirstName may not be longer than {MaxLengths.Employee.FirstName} characters");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Last_Name()
    {
        var command = CreateTestCommand(lastName: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.EmptyLastName,
            "LastName may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Last_Name_Exceeds_Max_Length()
    {
        var command = CreateTestCommand(lastName: new string('a', MaxLengths.Employee.LastName + 1));

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Employee.LastNameExceedsMaxLength,
            $"LastName may not be longer than {MaxLengths.Employee.LastName} characters");
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

    private static CreateEmployeeCommand CreateTestCommand(
        Guid? userId = null,
        Guid? storeId = null,
        string? email = null,
        string? firstName = null,
        string? lastName = null,
        string? mobile = null,
        string? password = null)
    {
        return new CreateEmployeeCommand(
            userId ?? Guid.NewGuid(),
            storeId ?? Guid.NewGuid(),
            email ?? "test@email.com",
            firstName ?? "test",
            lastName ?? "email",
            mobile ?? "09091234567",
            password ?? "Po=PF]PC6t.?8?ks)A6W");
    }
}