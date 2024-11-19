using System;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Employees.CreateEmployee;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Shared.Events.Employee;
using NSubstitute;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.CreateUser;

public sealed class CreateItemCommandHandlerTests
{
    private readonly CreateItemCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_User()
    {
        _fixture.SetupCurrentUser();

        var user = _fixture.SetupUser();

        var command = new CreateEmployeeCommand(
            Guid.NewGuid(),
            null,
            "test@email.com",
            "Test",
            "Email",
            "09091234567",
            "Po=PF]PC6t.?8?ks)A6W");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<EmployeeCreatedEvent>(x => x.AggregateId == command.EmployeeId);
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_User()
    {
        _fixture.SetupCurrentUser();

        var user = _fixture.SetupUser();

        var command = new CreateEmployeeCommand(
            user.Id,
            null,
            "test@email.com",
            "Test",
            "Email",
            "09091234567",
            "Po=PF]PC6t.?8?ks)A6W");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Employee.AlreadyExists,
                $"There is already a user with Id {command.EmployeeId}");
    }

    [Fact]
    public async Task Should_Not_Create_Already_Existing_Email()
    {
        _fixture.SetupCurrentUser();

        _fixture.UserRepository
            .GetByEmailAsync(Arg.Is<string>(y => y == "test@email.com"))
            .Returns(new Entities.Employee(
                Guid.NewGuid(),
                null,
                "max@mustermann.com",
                "Max",
                "Mustermann",
                "09091234567",
                "Password",
                DateTime.Now));

        var command = new CreateEmployeeCommand(
            Guid.NewGuid(),
            null,
            "test@email.com",
            "Test",
            "Email",
            "09091234567",
            "Po=PF]PC6t.?8?ks)A6W");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Employee.AlreadyExists,
                $"There is already a user with email {command.Email}");
    }

    [Fact]
    public async Task Should_Not_Create_User_Insufficient_Permissions()
    {
        _fixture.SetupUser();

        var command = new CreateEmployeeCommand(
            Guid.NewGuid(),
            null,
            "test@email.com",
            "Test",
            "Email",
            "09091234567",
            "Po=PF]PC6t.?8?ks)A6W");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                "You are not allowed to create users");
    }
}