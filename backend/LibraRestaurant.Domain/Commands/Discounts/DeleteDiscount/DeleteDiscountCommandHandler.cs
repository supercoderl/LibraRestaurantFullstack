
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
using LibraRestaurant.Shared.Events.Role;
using LibraRestaurant.Shared.Events.Discount;

namespace LibraRestaurant.Domain.Commands.Discounts.DeleteDiscount
{
    public sealed class DeleteDiscountCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteDiscountCommand>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IDiscountRepository discountRepository) : base(bus, unitOfWork, notifications)
        {
            _discountRepository = discountRepository;
        }

        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var discount = await _discountRepository.GetByIdAsync(request.DiscountId);

            if (discount is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no discount with Id {request.DiscountId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _discountRepository.Remove(discount);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new DiscountDeletedEvent(request.DiscountId));
            }
        }
    }
}
