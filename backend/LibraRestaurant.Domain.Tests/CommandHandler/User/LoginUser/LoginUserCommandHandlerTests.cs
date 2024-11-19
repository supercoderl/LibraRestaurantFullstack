﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Employees.LoginEmployee;
using LibraRestaurant.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.LoginUser;

public sealed class LoginUserCommandHandlerTests
{
    private readonly LoginUserCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Login_User()
    {
        await Task.CompletedTask;
        /*        var user = _fixture.SetupUser();

                var command = new LoginEmployeeCommand(user.Email, "z8]tnayvd5FNLU9:]AQm");

                var token = await _fixture.CommandHandler.Handle(command, default);

                _fixture.VerifyNoDomainNotification();

                token.Should().NotBeNullOrEmpty();

                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadToken(token) as JwtSecurityToken;

                var userIdClaim = decodedToken!.Claims
                    .FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.NameIdentifier));

                Guid.Parse(userIdClaim!.Value).Should().Be(user.Id);

                var userEmailClaim = decodedToken.Claims
                    .FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.Email));

                userEmailClaim!.Value.Should().Be(user.Email);*/

        /*        var userRoleClaim = decodedToken.Claims
                    .FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.Role));

                userRoleClaim!.Value.Should().Be(user.Role.ToString());*/
    }

    [Fact]
    public async Task Should_Not_Login_User_No_User()
    {
        await Task.CompletedTask;
        /*        var command = new LoginEmployeeCommand("test@email.com", "z8]tnayvd5FNLU9:]AQm");

                var token = await _fixture.CommandHandler.Handle(command, default);

                _fixture
                    .VerifyAnyDomainNotification()
                    .VerifyExistingNotification(
                        ErrorCodes.ObjectNotFound,
                        $"There is no user with email {command.Email}");

                token.Should().BeEmpty();*/
    }

    [Fact]
    public async Task Should_Not_Login_User_Incorrect_Password()
    {
        await Task.CompletedTask;
        /*        var user = _fixture.SetupUser();

                var command = new LoginEmployeeCommand(user.Email, "z8]tnayvd5FNLU9:]AQw");

                var token = await _fixture.CommandHandler.Handle(command, default);

                _fixture
                    .VerifyAnyDomainNotification()
                    .VerifyExistingNotification(
                        DomainErrorCodes.Employee.PasswordIncorrect,
                        "The password is incorrect");

                token.Should().BeEmpty();*/
    }
}