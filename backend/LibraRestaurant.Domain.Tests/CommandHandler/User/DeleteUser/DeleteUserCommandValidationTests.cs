using System;
using LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;
using LibraRestaurant.Domain.Errors;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.DeleteUser;

public sealed class DeleteItemCommandValidationTests :
    ValidationTestBase<DeleteEmployeeCommand, DeleteEmployeeCommandValidation>
{
    public DeleteItemCommandValidationTests() : base(new DeleteEmployeeCommandValidation())
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

    private static DeleteEmployeeCommand CreateTestCommand(Guid? userId = null)
    {
        return new DeleteEmployeeCommand(userId ?? Guid.NewGuid());
    }
}