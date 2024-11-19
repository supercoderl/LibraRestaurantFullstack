using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Tokens.CreateToken;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace LibraRestaurant.Domain.Commands.Employees.LoginEmployee;

public sealed class LoginEmployeeCommandHandler : CommandHandlerBase,
    IRequestHandler<LoginEmployeeCommand, Object>
{
    private const double _expiryDurationMinutes = 60;
    private readonly TokenSettings _tokenSettings;

    private readonly IEmployeeRepository _employeeRepository;

    public LoginEmployeeCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEmployeeRepository employeeRepository,
        IOptions<TokenSettings> tokenSettings) : base(bus, unitOfWork, notifications)
    {
        _employeeRepository = employeeRepository;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<Object> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return string.Empty;
        }

        var employee = await _employeeRepository.GetByEmailAsync(request.Email);

        if (employee is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no employee with email {request.Email}",
                    ErrorCodes.ObjectNotFound));

            return string.Empty;
        }

        var passwordVerified = BC.Verify(request.Password, employee.Password);

        if (!passwordVerified)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    "The password is incorrect",
                    DomainErrorCodes.Employee.PasswordIncorrect));

            return string.Empty;
        }

        employee.SetStatus(Enums.UserStatus.Active);
        employee.SetLastLoggedinDate(DateTimeOffset.Now);

        string refreshToken = await BuildRefreshToken(employee, Bus);

        if (!await CommitAsync())
        {
            return string.Empty;
        }

        return new
        {
            AccessToken = BuildToken(employee, _tokenSettings),
            RefreshToken = refreshToken
        };
    }

    private static string BuildToken(Employee employee, TokenSettings tokenSettings)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.MobilePhone, employee.Mobile),
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new Claim(ClaimTypes.Name, employee.FullName),
        };

        if (employee.EmployeeRoles is not null && employee.EmployeeRoles.Any())
        {
            foreach (var role in employee.EmployeeRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
            }
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(tokenSettings.Secret));

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            tokenSettings.Issuer,
            tokenSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_expiryDurationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private async static Task<string> BuildRefreshToken(Employee employee, IMediatorHandler bus)
    {
        var randomNumber = new Byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        string token = Convert.ToBase64String(randomNumber);

        await bus.SendCommandAsync(new CreateTokenCommand(
            0,
            token,
            employee.Id
        ));

        return token;
    }
}