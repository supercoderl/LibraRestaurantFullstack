
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

namespace LibraRestaurant.Domain.Commands.MenuItems.DeleteItem
{
    public sealed class DeleteItemCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteItemCommand>
    {
        private readonly IMenuItemRepository _itemRepository;

        public DeleteItemCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMenuItemRepository itemRepository) : base(bus, unitOfWork, notifications)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
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

            _itemRepository.Remove(item);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ItemDeletedEvent(request.ItemId));
            }
        }
    }
}
