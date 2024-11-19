
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
using LibraRestaurant.Shared.Events.PaymentHistory;

namespace LibraRestaurant.Domain.Commands.PaymentHistories.DeletePaymentHistory
{
    public sealed class DeletePaymentHistoryCommandHandler : CommandHandlerBase,
        IRequestHandler<DeletePaymentHistoryCommand>
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

        public DeletePaymentHistoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPaymentHistoryRepository paymentHistoryRepository) : base(bus, unitOfWork, notifications)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        public async Task Handle(DeletePaymentHistoryCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var paymentHistory = await _paymentHistoryRepository.GetByIdAsync(request.PaymentHistoryId);

            if (paymentHistory is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no paymentHistory with Id {request.PaymentHistoryId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _paymentHistoryRepository.Remove(paymentHistory);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PaymentHistoryDeletedEvent(request.PaymentHistoryId));
            }
        }
    }
}
