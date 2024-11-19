using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Store;
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
    public sealed class StoreEventHandler :
                INotificationHandler<StoreDeletedEvent>,
                INotificationHandler<StoreCreatedEvent>,
                INotificationHandler<StoreUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public StoreEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(StoreCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(StoreDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Store>(notification.AggregateId),
                cancellationToken);
        }

        public async Task Handle(StoreUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Store>(notification.AggregateId),
                cancellationToken);
        }
    }
}
