
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

namespace LibraRestaurant.Application.Queries.MenuItems.GetBySlug
{
    public sealed class GetItemBySlugQueryHandler :
        IRequestHandler<GetItemBySlugQuery, ItemViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IMenuItemRepository _itemRepository;

        public GetItemBySlugQueryHandler(IMenuItemRepository itemRepository, IMediatorHandler bus)
        {
            _itemRepository = itemRepository;
            _bus = bus;
        }

        public async Task<ItemViewModel?> Handle(GetItemBySlugQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetBySlugAsync(request.Slug);

            if (item is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetItemBySlugQuery),
                        $"Item with slug - {request.Slug} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return ItemViewModel.FromItem(item);
        }
    }
}
