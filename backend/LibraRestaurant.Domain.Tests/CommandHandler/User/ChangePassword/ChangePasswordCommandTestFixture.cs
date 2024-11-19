using System;
using LibraRestaurant.Domain.Commands.Employees.ChangePassword;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using NSubstitute;
using BC = BCrypt.Net.BCrypt;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.ChangePassword;

public sealed class ChangePasswordCommandTestFixture : CommandHandlerFixtureBase
{
    public ChangePasswordCommandHandler CommandHandler { get; }
    private IEmployeeRepository UserRepository { get; }

    public ChangePasswordCommandTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();

        CommandHandler = new ChangePasswordCommandHandler(
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
            BC.HashPassword("z8]tnayvd5FNLU9:]AQm"),
            DateTime.Now);

        Employee.GetEmployeeId().Returns(user.Id);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == user.Id))
            .Returns(user);

        return user;
    }

    public Guid SetupMissingUser()
    {
        var id = Guid.NewGuid();
        Employee.GetEmployeeId().Returns(id);
        return id;
    }
}