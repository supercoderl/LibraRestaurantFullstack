using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Employees.ChangePassword;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Shared.Events.Employee;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.ChangePassword;

public sealed class ChangePasswordCommandHandlerTests
{
    private readonly ChangePasswordCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Change_Password()
    {
        var user = _fixture.SetupUser();

        var command = new ChangePasswordCommand("z8]tnayvd5FNLU9:]AQm", "z8]tnayvd5FNLU9:]AQw");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<PasswordChangedEvent>(x => x.AggregateId == user.Id);
    }

    [Fact]
    public async Task Should_Not_Change_Password_No_User()
    {
        var userId = _fixture.SetupMissingUser();

        var command = new ChangePasswordCommand("z8]tnayvd5FNLU9:]AQm", "z8]tnayvd5FNLU9:]AQw");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no user with Id {userId}");
    }

    [Fact]
    public async Task Should_Not_Change_Password_Incorrect_Password()
    {
        _fixture.SetupUser();

        var command = new ChangePasswordCommand("z8]tnayvd5FNLU9:]AQw", "z8]tnayvd5FNLU9:]AQx");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<EmployeeUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Employee.PasswordIncorrect,
                "The password is incorrect");
    }
}