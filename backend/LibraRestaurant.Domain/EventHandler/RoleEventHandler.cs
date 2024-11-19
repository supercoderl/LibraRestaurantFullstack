using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Role;
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
    public sealed class RoleEventHandler :
                INotificationHandler<RoleDeletedEvent>,
                INotificationHandler<RoleCreatedEvent>,
                INotificationHandler<RoleUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public RoleEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(RoleCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(RoleDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Role>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(RoleUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Role>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
