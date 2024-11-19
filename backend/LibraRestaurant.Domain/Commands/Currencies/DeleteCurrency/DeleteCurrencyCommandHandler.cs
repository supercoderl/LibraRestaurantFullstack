
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
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Currency;

namespace LibraRestaurant.Domain.Commands.Currencies.DeleteCurrency
{
    public sealed class DeleteCurrencyCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public DeleteCurrencyCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICurrencyRepository currencyRepository) : base(bus, unitOfWork, notifications)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
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

            _currencyRepository.Remove(currency);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CurrencyDeletedEvent(request.CurrencyId));
            }
        }
    }
}
