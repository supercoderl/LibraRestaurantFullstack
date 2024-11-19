
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Role;
using LibraRestaurant.Shared.Events.DiscountType;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.CreateDiscountType
{
    public sealed class CreateDiscountTypeCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateDiscountTypeCommand>
    {
        private readonly IDiscountTypeRepository _discountTypeRepository;

        public CreateDiscountTypeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IDiscountTypeRepository discountTypeRepository) : base(bus, unitOfWork, notifications)
        {
            _discountTypeRepository = discountTypeRepository;
        }

        public async Task Handle(CreateDiscountTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var discountType = new Entities.DiscountType(
                request.DiscountTypeId,
                request.Name,
                request.Description,
                request.IsPercentage,
                request.Value,
                request.CreatedAt,
                request.StartTime,
                request.EndTime,
                request.CounponCode,
                request.MinOrderValue,
                request.MinItemQuantity,
                request.MaxDiscountValue);

            _discountTypeRepository.Add(discountType);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new DiscountTypeCreatedEvent(discountType.DiscountTypeId));
            }
        }
    }
}
