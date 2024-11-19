using System.Linq;
using System.Threading.Tasks;
using LibraRestaurant.Application.Queries.Employees.GetAll;
using LibraRestaurant.Application.Tests.Fixtures.Queries.Users;
using LibraRestaurant.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace LibraRestaurant.Application.Tests.Queries.Users;

public sealed class GetAllUsersQueryHandlerTests
{
    private readonly GetAllUsersTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_All_Users()
    {
        var user = _fixture.SetupUserAsync();

        var query = new PageQuery
        {
            PageSize = 1,
            Page = 1
        };

        var result = await _fixture.Handler.Handle(
            new GetAllEmployeesQuery(query, false, user.Email),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        var userViewModels = result.Items.ToArray();
        userViewModels.Should().NotBeNull();
        userViewModels.Should().ContainSingle();
        userViewModels.FirstOrDefault()!.Id.Should().Be(_fixture.ExistingUserId);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Users()
    {
        _fixture.SetupDeletedUserAsync();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.Handler.Handle(
            new GetAllEmployeesQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().BeEmpty();
    }
}