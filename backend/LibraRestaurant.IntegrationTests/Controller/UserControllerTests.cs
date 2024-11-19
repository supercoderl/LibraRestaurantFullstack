using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.IntegrationTests.Extensions;
using LibraRestaurant.IntegrationTests.Fixtures;
using LibraRestaurant.IntegrationTests.Infrastructure.Auth;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace LibraRestaurant.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class UserControllerTests : IClassFixture<UserTestFixture>
{
    private readonly UserTestFixture _fixture;

    public UserControllerTests(UserTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_All_User()
    {
        var response = await _fixture.ServerClient.GetAsync("/api/v1/user");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<EmployeeViewModel>>();

        message?.Data.Should().NotBeNull();

        var content = message!.Data!.Items.ToList();

        content.Count.Should().Be(2);

        var currentUser = content.First(x => x.Id == TestAuthenticationOptions.TestUserId);

        currentUser.Mobile.Should().Be(TestAuthenticationOptions.Mobile);
        currentUser.Email.Should().Be(TestAuthenticationOptions.Email);
        currentUser.FirstName.Should().Be(TestAuthenticationOptions.FirstName);
        currentUser.LastName.Should().Be(TestAuthenticationOptions.LastName);
    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_User_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync("/api/v1/user/" + TestAuthenticationOptions.TestUserId);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<EmployeeViewModel>();

        message?.Data.Should().NotBeNull();

        var content = message!.Data!;

        content.Id.Should().Be(TestAuthenticationOptions.TestUserId);
        content.Email.Should().Be(TestAuthenticationOptions.Email);
        content.FirstName.Should().Be(TestAuthenticationOptions.FirstName);
        content.LastName.Should().Be(TestAuthenticationOptions.LastName);
    }

    [Fact]
    [Priority(10)]
    public async Task Should_Create_User()
    {
        var user = new CreateEmployeeViewModel(
            null,
            "some@user.com",
            "Test",
            "Email",
            "09091234567",
            DateTime.Now);

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/user", user);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        message?.Data.Should().NotBeEmpty();
    }

    [Fact]
    [Priority(15)]
    public async Task Should_Login_User()
    {
        var user = new LoginEmployeeViewModel(
            "admin@email.com",
            "!Password123#");

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/user/login", user);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<string>();
        message?.Data.Should().NotBeEmpty();
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Get_The_Current_Active_Users()
    {
        var response = await _fixture.ServerClient.GetAsync("/api/v1/user/me");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<EmployeeViewModel>();

        message?.Data.Should().NotBeNull();

        var content = message!.Data!;

        content.Id.Should().Be(TestAuthenticationOptions.TestUserId);
        content.Email.Should().Be(TestAuthenticationOptions.Email);
        content.FirstName.Should().Be(TestAuthenticationOptions.FirstName);
        content.LastName.Should().Be(TestAuthenticationOptions.LastName);
    }

    [Fact]
    [Priority(25)]
    public async Task Should_Update_User()
    {
        var user = new UpdateEmployeeViewModel(
            Ids.Seed.EmployeeId,
            null,
            "newtest@email.com",
            "NewTest",
            "NewEmail",
            UserStatus.Active,
            "09091234567");

        var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/user", user);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<UpdateEmployeeViewModel>();

        message?.Data.Should().NotBeNull();

        var content = message!.Data;

        content.Should().BeEquivalentTo(user);

        // Check if user is really updated
        var userResponse = await _fixture.ServerClient.GetAsync("/api/v1/user/" + user.Id);

        userResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var userMessage = await userResponse.Content.ReadAsJsonAsync<EmployeeViewModel>();

        userMessage?.Data.Should().NotBeNull();

        var userContent = userMessage!.Data!;

        userContent.Id.Should().Be(user.Id);
        userContent.Email.Should().Be(user.Email);
        userContent.FirstName.Should().Be(user.FirstName);
        userContent.LastName.Should().Be(user.LastName);
        userContent.Mobile.Should().Be(user.Mobile);
    }

    [Fact]
    [Priority(30)]
    public async Task Should_Change_User_Password()
    {
        var user = new ChangePasswordViewModel(
            "!Password123#",
            "!Password123#1");

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/user/changePassword", user);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<ChangePasswordViewModel>();

        message?.Data.Should().NotBeNull();

        var content = message!.Data;

        content.Should().BeEquivalentTo(user);

        // Verify the user can login with the new password
        var login = new LoginEmployeeViewModel(
            TestAuthenticationOptions.Email,
            user.NewPassword);

        var loginResponse = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/user/login", login);

        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var loginMessage = await loginResponse.Content.ReadAsJsonAsync<string>();

        loginMessage?.Data.Should().NotBeEmpty();
    }

    [Fact]
    [Priority(35)]
    public async Task Should_Delete_User()
    {
        var response = await _fixture.ServerClient.DeleteAsync("/api/v1/user/" + TestAuthenticationOptions.TestUserId);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();

        message?.Data.Should().NotBeEmpty();

        var content = message!.Data;
        content.Should().Be(TestAuthenticationOptions.TestUserId);

        var userResponse = await _fixture.ServerClient.GetAsync("/api/v1/user/" + TestAuthenticationOptions.TestUserId);

        userResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}