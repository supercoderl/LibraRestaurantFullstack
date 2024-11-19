using System;
using LibraRestaurant.Domain.Commands.Employees.LoginEmployee;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Settings;
using Microsoft.Extensions.Options;
using NSubstitute;
using BC = BCrypt.Net.BCrypt;

namespace LibraRestaurant.Domain.Tests.CommandHandler.User.LoginUser;

public sealed class LoginUserCommandTestFixture : CommandHandlerFixtureBase
{
    public LoginEmployeeCommandHandler CommandHandler { get; set; }
    public IEmployeeRepository UserRepository { get; set; }
    public IOptions<TokenSettings> TokenSettings { get; set; }

    public LoginUserCommandTestFixture()
    {
        UserRepository = Substitute.For<IEmployeeRepository>();

        TokenSettings = Options.Create(new TokenSettings
        {
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            Secret = "asjdlkasjd87439284)@#(*asjdlkasjd87439284)@#(*"
        });

        CommandHandler = new LoginEmployeeCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            UserRepository,
            TokenSettings);
    }

    public Entities.Employee SetupUser()
    {
        var user = new Entities.Employee(
            Guid.NewGuid(),
            null,
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "01234567090",
            BC.HashPassword("z8]tnayvd5FNLU9:]AQm"),
            DateTime.Now);

        Employee.GetEmployeeId().Returns(user.Id);

        UserRepository
            .GetByEmailAsync(Arg.Is<string>(y => y == user.Email))
            .Returns(user);

        return user;
    }
}