using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.OrderHead;
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
    public sealed class OrderEventHandler :
                INotificationHandler<OrderDeletedEvent>,
                INotificationHandler<OrderCreatedEvent>,
                INotificationHandler<OrderUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public OrderEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<OrderHeader>(notification.AggregateId),
                cancellationToken);
        }

        public async Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<OrderHeader>(notification.AggregateId),
                cancellationToken);
        }
    }
}
