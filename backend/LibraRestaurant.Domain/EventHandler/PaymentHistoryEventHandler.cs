using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.PaymentHistory;
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
    public sealed class PaymentHistoryEventHandler :
                INotificationHandler<PaymentHistoryDeletedEvent>,
                INotificationHandler<PaymentHistoryCreatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public PaymentHistoryEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(PaymentHistoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PaymentHistoryDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<PaymentHistory>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
