
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
using LibraRestaurant.Shared.Events.Role;
using LibraRestaurant.Shared.Events.DiscountType;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.UpdateDiscountType
{
    public sealed class UpdateDiscountTypeCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateDiscountTypeCommand>
    {
        private readonly IDiscountTypeRepository _discountTypeRepository;

        public UpdateDiscountTypeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IDiscountTypeRepository discountTypeRepository) : base(bus, unitOfWork, notifications)
        {
            _discountTypeRepository = discountTypeRepository;
        }

        public async Task Handle(UpdateDiscountTypeCommand request, CancellationToken cancellationToken)
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
                        $"There is no discount type with Id {request.DiscountTypeId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            discountType.SetName(request.Name);
            discountType.SetDescription(request.Description);
            discountType.SetValue(request.Value);
            discountType.SetIsPercentage(request.IsPercentage);
            discountType.SetStartTime(request.StartTime);
            discountType.SetEndTime(request.EndTime);
            discountType.SetCounponCode(request.CounponCode);
            discountType.SetMinOrderValue(request.MinOrderValue);
            discountType.SetMinItemQuantity(request.MinItemQuantity);
            discountType.SetMaxDiscountValue(request.MaxDiscountValue);

            _discountTypeRepository.Update(discountType);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new DiscountTypeUpdatedEvent(discountType.DiscountTypeId));
            }
        }
    }
}
