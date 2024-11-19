
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
using LibraRestaurant.Domain.Commands.CategoryItems.UpsertCategoryItem;

namespace LibraRestaurant.Domain.Commands.MenuItems.UpdateItem
{
    public sealed class UpdateItemCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateItemCommand>
    {
        private readonly IMenuItemRepository _itemRepository;

        public UpdateItemCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMenuItemRepository itemRepository) : base(bus, unitOfWork, notifications)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var item = await _itemRepository.GetByIdAsync(request.ItemId);

            if (item is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no item with Id {request.ItemId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            item.SetTitle(request.Title);
            item.SetSlug(request.Slug);
            item.SetSKU(request.SKU);
            item.SetSummary(request.Summary);
            item.SetPrice(request.Price);
            item.SetQuantity(request.Quantity);
            item.SetRecipe(request.Recipe);
            item.SetPicture(request.Picture);
            item.SetInstruction(request.Instruction);
            item.SetLastUpdatedAt(DateTime.Now);

            _itemRepository.Update(item);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ItemUpdatedEvent(item.ItemId));
                await Bus.SendCommandAsync(new UpsertCategoryItemCommand(0, request.CategoryIds, item.ItemId, null));
            }
        }
    }
}
