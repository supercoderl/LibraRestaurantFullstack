using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.DiscountType;
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
    public sealed class DiscountTypeEventHandler :
                    INotificationHandler<DiscountTypeDeletedEvent>,
                    INotificationHandler<DiscountTypeCreatedEvent>,
                    INotificationHandler<DiscountTypeUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public DiscountTypeEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(DiscountTypeCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(DiscountTypeDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<DiscountType>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(DiscountTypeUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<DiscountType>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
