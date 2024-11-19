using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Category;
using LibraRestaurant.Shared.Events.CategoryItem;
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
    public sealed class CategoryItemEventHandler :
                    INotificationHandler<CategoryItemDeletedEvent>,
                    INotificationHandler<CategoryItemCreatedEvent>,
                    INotificationHandler<CategoryItemUpdatedEvent>,
                    INotificationHandler<CategoryItemUpsertEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public CategoryItemEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(CategoryItemCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(CategoryItemDeletedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<CategoryItem>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(CategoryItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<CategoryItem>(notification.AggregateNumberId),
                cancellationToken);
        }

        public Task Handle(CategoryItemUpsertEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
