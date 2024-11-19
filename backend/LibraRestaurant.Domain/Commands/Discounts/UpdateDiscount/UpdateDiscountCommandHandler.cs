
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
using LibraRestaurant.Shared.Events.Discount;

namespace LibraRestaurant.Domain.Commands.Discounts.UpdateDiscount
{
    public sealed class UpdateDiscountCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateDiscountCommand>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IDiscountRepository discountRepository) : base(bus, unitOfWork, notifications)
        {
            _discountRepository = discountRepository;
        }

        public async Task Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
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

            discount.SetDiscountTypeId(request.DiscountTypeId);
            discount.SetCategoryId(request.CategoryId);
            discount.SetItemId(request.ItemId);
            discount.SetComments(request.Comments);
            discount.SetDiscountTargetType(request.DiscountTargetType);
            discount.SetOrderId(request.OrderId);
            discount.SetInvoiceId(request.InvoiceId);

            _discountRepository.Update(discount);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new DiscountUpdatedEvent(discount.DiscountId));
            }
        }
    }
}
