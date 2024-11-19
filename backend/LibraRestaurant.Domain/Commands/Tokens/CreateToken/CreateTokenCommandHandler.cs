
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Token;
using System;

namespace LibraRestaurant.Domain.Commands.Tokens.CreateToken
{
    public sealed class CreateTokenCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateTokenCommand>
    {
        private readonly ITokenRepository _tokenRepository;

        public CreateTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITokenRepository tokenRepository) : base(bus, unitOfWork, notifications)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var token = new Entities.Token(
                request.TokenId,
                request.OldToken,
                request.EmployeeId,
                true,
                null,
                DateTime.Now.AddDays(45));

            _tokenRepository.Add(token);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TokenCreatedEvent(token.TokenId));
            }
        }
    }
}
