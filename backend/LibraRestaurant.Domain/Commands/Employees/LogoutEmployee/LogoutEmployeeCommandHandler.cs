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
using LibraRestaurant.Shared.Events.Employee;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraRestaurant.Domain.Commands.Employees.LogoutEmployee;

public sealed class LogoutEmployeeCommandHandler : CommandHandlerBase,
    IRequestHandler<LogoutEmployeeCommand, string>
{
    private readonly ITokenRepository _tokenRepository;

    public LogoutEmployeeCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ITokenRepository tokenRepository) : base(bus, unitOfWork, notifications)
    {
        _tokenRepository = tokenRepository;
    }

    public async Task<string> Handle(LogoutEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return "Đăng xuất thất bại";
        }

        var token = await _tokenRepository.GetByOldTokenAsync(request.RefreshToken);

        if (token is null)
        {
            return "Đăng xuất thành công";
        }

        token.SetIsActive(false);
        token.SetRevokedAt(DateTime.Now);

        _tokenRepository.Update(token);

        if (await CommitAsync())
        {
            /*await Bus.RaiseEventAsync(new EmployeeUpdatedEvent(token.TokenId));*/
            return "Đăng xuất thành công";
        }

        return "Đăng xuất thành công";
    }
}