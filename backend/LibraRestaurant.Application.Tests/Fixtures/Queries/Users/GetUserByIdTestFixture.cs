using System;
using System.Linq;
using LibraRestaurant.Application.Queries.Employees.GetEmployeeById;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace LibraRestaurant.Application.Tests.Fixtures.Queries.Users;

public sealed class GetUserByIdTestFixture : QueryHandlerBaseFixture
{
    private IEmployeeRepository UserRepository { get; }
    public GetEmployeeByIdQueryHandler Handler { get; }
    public Guid ExistingUserId { get; } = Guid.NewGuid();

    public GetUserByIdTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();

        Handler = new GetEmployeeByIdQueryHandler(UserRepository, Bus);
    }

    public void SetupUserAsync()
    {
        var user = new Employee(
            ExistingUserId,
            null,
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "09091234567",
            "Password",
            DateTime.Now);

        UserRepository.GetByIdAsync(Arg.Is<Guid>(y => y == ExistingUserId)).Returns(user);
    }

    public void SetupDeletedUserAsync()
    {
        var user = new Employee(
            ExistingUserId,
            null,
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "09091234567",
            "Password",
            DateTime.Now);

        user.Delete();

        var query = new[] { user }.AsQueryable().BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);
    }
}