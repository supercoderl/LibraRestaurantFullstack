using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
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
    public sealed class MenuEventHandler :
            INotificationHandler<MenuDeletedEvent>,
            INotificationHandler<MenuCreatedEvent>,
            INotificationHandler<MenuUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public MenuEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(MenuCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(MenuDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Menu>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(MenuUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Menu>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
