using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.OrderLine;
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
    public sealed class OrderLineEventHandler :
                INotificationHandler<OrderLineDeletedEvent>,
                INotificationHandler<OrderLineCreatedEvent>,
                INotificationHandler<OrderLineUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public OrderLineEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(OrderLineCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderLineDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<OrderLine>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(OrderLineUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<OrderLine>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
