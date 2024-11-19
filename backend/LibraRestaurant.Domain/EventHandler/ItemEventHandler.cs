
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.MenuItem;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.EventHandler
{
    public sealed class ItemEventHandler :
        INotificationHandler<ItemDeletedEvent>,
        INotificationHandler<ItemCreatedEvent>,
        INotificationHandler<ItemUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public ItemEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(ItemCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(ItemDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<MenuItem>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(ItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<MenuItem>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
