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

namespace LibraRestaurant.Domain.Commands.Employees.RefreshEmployee;

public sealed class RefreshEmployeeCommandHandler : CommandHandlerBase,
    IRequestHandler<RefreshEmployeeCommand, Object>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITokenRepository _tokenRepository;

    private const double _expiryDurationMinutes = 60;
    private readonly TokenSettings _tokenSettings;

    public RefreshEmployeeCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEmployeeRepository employeeRepository,
        ITokenRepository tokenRepository,
        IOptions<TokenSettings> tokenSettings) : base(bus, unitOfWork, notifications)
    {
        _employeeRepository = employeeRepository;
        _tokenRepository = tokenRepository;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<Object> Handle(RefreshEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return "";
        }

        var token = await _tokenRepository.GetByOldTokenAsync(request.RefreshToken);

        if (token is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no token with refresh token {request.RefreshToken}",
                    ErrorCodes.ObjectNotFound));

            return "";
        }

        else if (DateTime.Compare(token.ExpireDate, DateTime.Now) == 0)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"Token was expired!",
                    ErrorCodes.ObjectNotFound));

            return "";
        }

        else if(!token.IsActive)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"You cannot use this token to refresh!",
                    ErrorCodes.ObjectNotFound));

            return "";
        }

        else if(token.RevokedAt is not null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"Token was revoked!",
                    ErrorCodes.ObjectNotFound));

            return "";
        }

        var employee = await _employeeRepository.GetByIdAsync(token.EmployeeId);

        if (employee is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no employee with id {token.EmployeeId}",
                    ErrorCodes.ObjectNotFound));

            return "";
        }

        return new
        {
            AccessToken = BuildToken(employee, _tokenSettings),
            RefreshToken = await BuildRefreshToken(employee, Bus)
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
            new Claim("jti", Guid.NewGuid().ToString())
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
            expires: DateTime.Now.AddMinutes(_expiryDurationMinutes + new Random().Next(1, 2)),
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