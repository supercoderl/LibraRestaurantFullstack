using System;
using LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using NSubstitute;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.DeleteUser;

public sealed class DeleteItemCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteEmployeeCommandHandler CommandHandler { get; }
    private IEmployeeRepository UserRepository { get; }

    public DeleteItemCommandTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();

        CommandHandler = new DeleteEmployeeCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            UserRepository,
            Employee);
    }

    public Entities.Employee SetupUser()
    {
        var user = new Entities.Employee(
            Guid.NewGuid(),
            null,
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "09091234567",
            "Password",
            DateTime.Now);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == user.Id))
            .Returns(user);

        return user;
    }
}