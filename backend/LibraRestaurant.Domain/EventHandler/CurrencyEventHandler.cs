using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Category;
using LibraRestaurant.Shared.Events.Currency;
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
    public sealed class CurrencyEventHandler :
                INotificationHandler<CurrencyDeletedEvent>,
                INotificationHandler<CurrencyCreatedEvent>,
                INotificationHandler<CurrencyUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public CurrencyEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(CurrencyCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(CurrencyDeletedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Currency>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(CurrencyUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Currency>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
