using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.PaymentMethod;
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
    public sealed class PaymentMethodEventHandler :
                INotificationHandler<PaymentMethodDeletedEvent>,
                INotificationHandler<PaymentMethodCreatedEvent>,
                INotificationHandler<PaymentMethodUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public PaymentMethodEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(PaymentMethodCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PaymentMethodDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<PaymentMethod>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(PaymentMethodUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<PaymentMethod>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
