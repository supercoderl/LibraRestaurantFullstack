
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Role;
using LibraRestaurant.Shared.Events.Discount;

namespace LibraRestaurant.Domain.Commands.Discounts.CreateDiscount
{
    public sealed class CreateDiscountCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateDiscountCommand>
    {
        private readonly IDiscountRepository _discountRepository;

        public CreateDiscountCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IDiscountRepository discountRepository) : base(bus, unitOfWork, notifications)
        {
            _discountRepository = discountRepository;
        }

        public async Task Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var discount = new Entities.Discount(
                request.DiscountId,
                request.DiscountTypeId,
                request.DiscountTargetType,
                request.CategoryId,
                request.ItemId,
                request.OrderId,
                request.InvoiceId,
                request.Comments);

            _discountRepository.Add(discount);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new DiscountCreatedEvent(discount.DiscountId));
            }
        }
    }
}
