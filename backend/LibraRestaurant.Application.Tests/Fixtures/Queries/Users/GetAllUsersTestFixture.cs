using System;
using LibraRestaurant.Application.Queries.Employees.GetAll;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace LibraRestaurant.Application.Tests.Fixtures.Queries.Users;

public sealed class GetAllUsersTestFixture : QueryHandlerBaseFixture
{
    private IEmployeeRepository UserRepository { get; }
    public GetAllEmployeesQueryHandler Handler { get; }
    public Guid ExistingUserId { get; } = Guid.NewGuid();

    public GetAllUsersTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();
        var sortingProvider = new EmployeeViewModelSortProvider();

        Handler = new GetAllEmployeesQueryHandler(UserRepository, sortingProvider);
    }

    public Employee SetupUserAsync()
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

        var query = new[] { user }.BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);

        return user;
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

        var query = new[] { user }.BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);
    }
}