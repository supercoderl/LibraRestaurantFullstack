using System;
using System.Collections.Generic;
using LibraRestaurant.Application.gRPC;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace LibraRestaurant.gRPC.Tests.Fixtures;

public sealed class UserTestFixture
{
    private IEmployeeRepository UserRepository { get; } = Substitute.For<IEmployeeRepository>();

    public EmployeesApiImplementation UsersApiImplementation { get; }

    public IEnumerable<Employee> ExistingUsers { get; }

    public UserTestFixture()
    {
        ExistingUsers = new List<Employee>
        {
            new(
                Guid.NewGuid(),
                null,
                "test@test.de",
                "Test First Name",
                "Test Last Name",
                "Test Mobile",
                "Test Password",
                DateTime.Now),
            new(
                Guid.NewGuid(),
                null,
                "email@Email.de",
                "Email First Name",
                "Email Last Name",
                "Email Mobile",
                "Email Password",
                DateTime.Now),
            new(
                Guid.NewGuid(),
                null,
                "user@user.de",
                "User First Name",
                "User Last Name",
                "User Mobile",
                "User Password",
                DateTime.Now)
        };

        var queryable = ExistingUsers.BuildMock();

        UserRepository.GetAllNoTracking().Returns(queryable);

        UsersApiImplementation = new EmployeesApiImplementation(UserRepository);
    }
}