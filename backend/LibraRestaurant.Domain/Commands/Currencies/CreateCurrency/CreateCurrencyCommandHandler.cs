
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Currency;

namespace LibraRestaurant.Domain.Commands.Currencies.CreateCurrency
{
    public sealed class CreateCurrencyCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CreateCurrencyCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICurrencyRepository currencyRepository) : base(bus, unitOfWork, notifications)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var currency = new Entities.Currency(
                request.CurrencyId,
                request.Name,
                request.Description);

            _currencyRepository.Add(currency);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CurrencyCreatedEvent(currency.CurrencyId));
            }
        }
    }
}
