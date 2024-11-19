using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.Queries.Employees.GetEmployeeById;
using LibraRestaurant.Application.Tests.Fixtures.Queries.Users;
using LibraRestaurant.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace LibraRestaurant.Application.Tests.Queries.Users;

public sealed class GetUserByIdQueryHandlerTests
{
    private readonly GetUserByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_User()
    {
        _fixture.SetupUserAsync();

        var result = await _fixture.Handler.Handle(
            new GetEmployeeByIdQuery(_fixture.ExistingUserId),
            default);

        _fixture.VerifyNoDomainNotification();

        result.Should().NotBeNull();
        result!.Id.Should().Be(_fixture.ExistingUserId);
    }

    [Fact]
    public async Task Should_Raise_Notification_For_No_User()
    {
        _fixture.SetupUserAsync();

        var request = new GetEmployeeByIdQuery(Guid.NewGuid());
        var result = await _fixture.Handler.Handle(
            request,
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetEmployeeByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"User with id {request.Id} could not be found");

        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_User()
    {
        _fixture.SetupDeletedUserAsync();

        var result = await _fixture.Handler.Handle(
            new GetEmployeeByIdQuery(_fixture.ExistingUserId),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetEmployeeByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"User with id {_fixture.ExistingUserId} could not be found");

        result.Should().BeNull();
    }
}