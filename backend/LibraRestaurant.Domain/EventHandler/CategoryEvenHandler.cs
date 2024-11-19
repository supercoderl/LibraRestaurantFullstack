using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Category;
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
    public sealed class CategoryEventHandler :
                INotificationHandler<CategoryDeletedEvent>,
                INotificationHandler<CategoryCreatedEvent>,
                INotificationHandler<CategoryUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public CategoryEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Category>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Category>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
