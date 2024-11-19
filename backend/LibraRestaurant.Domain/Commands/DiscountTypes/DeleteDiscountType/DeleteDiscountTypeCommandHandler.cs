
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
using LibraRestaurant.Shared.Events.DiscountType;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.DeleteDiscountType
{
    public sealed class DeleteDiscountTypeCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteDiscountTypeCommand>
    {
        private readonly IDiscountTypeRepository _discountTypeRepository;

        public DeleteDiscountTypeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IDiscountTypeRepository discountTypeRepository) : base(bus, unitOfWork, notifications)
        {
            _discountTypeRepository = discountTypeRepository;
        }

        public async Task Handle(DeleteDiscountTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var discountType = await _discountTypeRepository.GetByIdAsync(request.DiscountTypeId);

            if (discountType is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no discountType with Id {request.DiscountTypeId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _discountTypeRepository.Remove(discountType);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new DiscountTypeDeletedEvent(request.DiscountTypeId));
            }
        }
    }
}
