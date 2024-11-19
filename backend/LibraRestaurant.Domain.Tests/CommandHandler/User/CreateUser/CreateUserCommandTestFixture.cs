using System;
using LibraRestaurant.Domain.Commands.Employees.CreateEmployee;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using NSubstitute;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.CreateUser;

public sealed class CreateItemCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateEmployeeCommandHandler CommandHandler { get; }
    public IEmployeeRepository UserRepository { get; }

    public CreateItemCommandTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();

        CommandHandler = new CreateEmployeeCommandHandler(
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

    public void SetupCurrentUser()
    {
        var userId = Guid.NewGuid();

        Employee.GetEmployeeId().Returns(userId);

        UserRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == userId))
            .Returns(new Entities.Employee(
                userId,
                null,
                "some email",
                "some first name",
                "some last name",
                "some mobile",
                "some password",
                DateTime.Now));
    }
}