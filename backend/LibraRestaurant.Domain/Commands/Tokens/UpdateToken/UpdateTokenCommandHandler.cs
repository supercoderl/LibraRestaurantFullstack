
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Token;

namespace LibraRestaurant.Domain.Commands.Tokens.UpdateToken
{
    public sealed class UpdateTokenCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateTokenCommand>
    {
        private readonly ITokenRepository _tokenRepository;

        public UpdateTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITokenRepository tokenRepository) : base(bus, unitOfWork, notifications)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var token = await _tokenRepository.GetByIdAsync(request.TokenId);

            if (token is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no token with Id {request.TokenId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            token.SetOldToken(request.OldToken);
            token.SetEmployee(request.EmployeeId);
            token.SetRevokedAt(request.RevokedAt);
            token.SetIsActive(request.IsActive);
            token.SetExpireDate(request.ExpireDate);

            _tokenRepository.Update(token);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TokenUpdatedEvent(token.TokenId));
            }
        }
    }
}
