using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraRestaurant.gRPC.Tests.Fixtures;
using LibraRestaurant.Proto.Employees;
using FluentAssertions;
using Xunit;

namespace LibraRestaurant.gRPC.Tests.Users;

public sealed class GetUsersByIdsTests : IClassFixture<UserTestFixture>
{
    private readonly UserTestFixture _fixture;

    public GetUsersByIdsTests(UserTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
    {
        var result = await _fixture.UsersApiImplementation.GetByIds(
            SetupRequest(Enumerable.Empty<Guid>()),
            default!);

        result.Employees.Should().HaveCount(0);
    }

    [Fact]
    public async Task Should_Get_Requested_Users()
    {
        var nonExistingId = Guid.NewGuid();

        var ids = _fixture.ExistingUsers
            .Take(2)
            .Select(user => user.Id)
            .ToList();

        ids.Add(nonExistingId);

        var result = await _fixture.UsersApiImplementation.GetByIds(
            SetupRequest(ids),
            default!);

        result.Employees.Should().HaveCount(2);

        foreach (var user in result.Employees)
        {
            var userId = Guid.Parse(user.Id);

            userId.Should().NotBe(nonExistingId);

            var mockUser = _fixture.ExistingUsers.First(u => u.Id == userId);

            mockUser.Should().NotBeNull();

            user.Email.Should().Be(mockUser.Email);
            user.FirstName.Should().Be(mockUser.FirstName);
            user.LastName.Should().Be(mockUser.LastName);
        }
    }

    private static GetEmployeesByIdsRequest SetupRequest(IEnumerable<Guid> ids)
    {
        var request = new GetEmployeesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));
        request.Ids.Add("Not a guid");

        return request;
    }
}