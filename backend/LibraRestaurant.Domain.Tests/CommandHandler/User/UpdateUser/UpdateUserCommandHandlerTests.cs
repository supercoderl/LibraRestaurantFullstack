using System;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Shared.Events.Employee;
using NSubstitute;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.UpdateUser;

public sealed class UpdateItemCommandHandlerTests
{
    private readonly UpdateItemCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Update_User()
    {
        var user = _fixture.SetupUser();

        var command = new UpdateEmployeeCommand(
            user.Id,
            Guid.NewGuid(),
            "test@email.com",
            "Test",
            "Email",
            UserStatus.Active,
            "09091234567");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<EmployeeUpdatedEvent>(x => x.AggregateId == command.EmployeeId);
    }

    [Fact]
    public async Task Should_Not_Update_Non_Existing_User()
    {
        _fixture.SetupUser();

        var command = new UpdateEmployeeCommand(
            Guid.NewGuid(),
            null,
            "test@email.com",
            "Test",
            "Email",
            UserStatus.Active,
            "09091234567");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no user with Id {command.EmployeeId}");
    }

    [Fact]
    public async Task Should_Not_Update_With_Existing_User_Email()
    {
        var user = _fixture.SetupUser();

        var command = new UpdateEmployeeCommand(
            user.Id,
            null,
            "test@email.com",
            "Test",
            "Email",
            UserStatus.Active,
            "09091234567");

        _fixture.UserRepository
            .GetByEmailAsync(command.Email)
            .Returns(new Entities.Employee(
                Guid.NewGuid(),
                null,
                command.Email,
                "Some",
                "User",
                "09091234567",
                "234fs@#*@#",
                DateTime.Now));

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Employee.AlreadyExists,
                $"There is already a user with email {command.Email}");
    }

    [Fact]
    public async Task Should_Not_Update_Admin_Properties()
    {
        var user = _fixture.SetupUser();
        _fixture.SetupCurrentUser(user.Id);

        var command = new UpdateEmployeeCommand(
            user.Id,
            null,
            "test@email.com",
            "Test",
            "Email",
            UserStatus.Active,
            "09091234567");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture.UserRepository.Received(1).Update(Arg.Is<Entities.Employee>(u =>
            u.Mobile == user.Mobile &&
            u.Id == command.EmployeeId &&
            u.Email == command.Email &&
            u.FirstName == command.FirstName));

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<EmployeeUpdatedEvent>(x => x.AggregateId == command.EmployeeId);
    }
}