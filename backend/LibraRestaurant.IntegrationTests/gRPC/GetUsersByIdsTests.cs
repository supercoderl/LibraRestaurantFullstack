using System.Linq;
using System.Threading.Tasks;
using LibraRestaurant.IntegrationTests.Fixtures.gRPC;
using LibraRestaurant.Proto.Employees;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace LibraRestaurant.IntegrationTests.gRPC;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class GetUsersByIdsTests : IClassFixture<GetUsersByIdsTestFixture>
{
    private readonly GetUsersByIdsTestFixture _fixture;

    public GetUsersByIdsTests(GetUsersByIdsTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Get_Users_By_Ids()
    {
        var client = new EmployeesApi.EmployeesApiClient(_fixture.GrpcChannel);

        var request = new GetEmployeesByIdsRequest();
        request.Ids.Add(_fixture.CreatedUserId.ToString());

        var response = await client.GetByIdsAsync(request);

        response.Employees.Should().HaveCount(1);

        var user = response.Employees.First();
        var createdUser = _fixture.CreateUser();

        user.Email.Should().Be(createdUser.Email);
        user.FirstName.Should().Be(createdUser.FirstName);
        user.LastName.Should().Be(createdUser.LastName);
    }
}