using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Currency;
using LibraRestaurant.Shared.Events.Customer;
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
    public sealed class CustomerEventHandler :
                    INotificationHandler<CustomerDeletedEvent>,
                    INotificationHandler<CustomerCreatedEvent>,
                    INotificationHandler<CustomerUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public CustomerEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Customer>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Customer>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
