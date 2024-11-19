
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
using LibraRestaurant.Shared.Events.Currency;

namespace LibraRestaurant.Domain.Commands.Currencies.UpdateCurrency
{
    public sealed class UpdateCurrencyCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public UpdateCurrencyCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICurrencyRepository currencyRepository) : base(bus, unitOfWork, notifications)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);

            if (currency is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no currency with Id {request.CurrencyId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            currency.SetName(request.Name);
            currency.SetDescription(request.Description);

            _currencyRepository.Update(currency);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CurrencyUpdatedEvent(currency.CurrencyId));
            }
        }
    }
}
