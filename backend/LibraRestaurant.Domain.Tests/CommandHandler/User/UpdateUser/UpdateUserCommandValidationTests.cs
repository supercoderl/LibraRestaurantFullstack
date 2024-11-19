using System;
using LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Errors;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.UpdateUser;

public sealed class UpdateItemCommandValidationTests :
    ValidationTestBase<UpdateEmployeeCommand, UpdateEmployeeCommandValidation>
{
    public UpdateItemCommandValidationTests() : base(new UpdateEmployeeCommandValidation())
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

    private static UpdateEmployeeCommand CreateTestCommand(
        Guid? userId = null,
        Guid? storeId = null,
        string? email = null,
        string? firstName = null,
        string? lastName = null,
        UserStatus? status = null,
        string? mobile = null)
    {
        return new UpdateEmployeeCommand(
            userId ?? Guid.NewGuid(),
            storeId ?? Guid.NewGuid(),
            email ?? "test@email.com",
            firstName ?? "test",
            lastName ?? "email",
            status ?? UserStatus.Active,
            mobile ?? "09091234567");
    }
}