using System;
using LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using NSubstitute;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.UpdateUser;

public sealed class UpdateItemCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateEmployeeCommandHandler CommandHandler { get; }
    public IEmployeeRepository UserRepository { get; }

    public UpdateItemCommandTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();

        CommandHandler = new UpdateEmployeeCommandHandler(
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

    public void SetupCurrentUser(Guid userId)
    {
        Employee.GetEmployeeId().Returns(userId);
    }
}