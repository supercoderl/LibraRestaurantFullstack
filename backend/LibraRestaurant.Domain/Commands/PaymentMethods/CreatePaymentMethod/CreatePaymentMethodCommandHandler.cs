
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.PaymentMethod;

namespace LibraRestaurant.Domain.Commands.PaymentMethods.CreatePaymentMethod
{
    public sealed class CreatePaymentMethodCommandHandler : CommandHandlerBase,
        IRequestHandler<CreatePaymentMethodCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public CreatePaymentMethodCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPaymentMethodRepository paymentMethodRepository) : base(bus, unitOfWork, notifications)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var paymentMethod = new Entities.PaymentMethod(
                request.PaymentMethodId,
                request.Name,
                request.Description,
                request.Picture,
                request.IsActive);

            _paymentMethodRepository.Add(paymentMethod);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PaymentMethodCreatedEvent(paymentMethod.PaymentMethodId));
            }
        }
    }
}
