
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
using LibraRestaurant.Shared.Events.PaymentMethod;

namespace LibraRestaurant.Domain.Commands.PaymentMethods.UpdatePaymentMethod
{
    public sealed class UpdatePaymentMethodCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdatePaymentMethodCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public UpdatePaymentMethodCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPaymentMethodRepository paymentMethodRepository) : base(bus, unitOfWork, notifications)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.PaymentMethodId);

            if (paymentMethod is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no paymentMethod with Id {request.PaymentMethodId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            paymentMethod.SetName(request.Name);
            paymentMethod.SetDescription(request.Description);
            paymentMethod.SetActive(request.IsActive);

            _paymentMethodRepository.Update(paymentMethod);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PaymentMethodUpdatedEvent(paymentMethod.PaymentMethodId));
            }
        }
    }
}
