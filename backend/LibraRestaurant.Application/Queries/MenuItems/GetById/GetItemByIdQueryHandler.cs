
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
using LibraRestaurant.Application.ViewModels.MenuItems;

namespace LibraRestaurant.Application.Queries.MenuItems.GetById
{
    public sealed class GetItemByIdQueryHandler :
        IRequestHandler<GetItemByIdQuery, ItemViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IMenuItemRepository _itemRepository;

        public GetItemByIdQueryHandler(IMenuItemRepository itemRepository, IMediatorHandler bus)
        {
            _itemRepository = itemRepository;
            _bus = bus;
        }

        public async Task<ItemViewModel?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id);

            if (item is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetItemByIdQuery),
                        $"Item with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return ItemViewModel.FromItem(item);
        }
    }
}
