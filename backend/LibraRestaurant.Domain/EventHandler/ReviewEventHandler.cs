using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.PaymentHistory;
using LibraRestaurant.Shared.Events.Review;
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
    public sealed class ReviewEventHandler :
                    INotificationHandler<ReviewDeletedEvent>,
                    INotificationHandler<ReviewCreatedEvent>,
                    INotificationHandler<ReviewUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public ReviewEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(ReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(ReviewDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Review>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(ReviewUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Review>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
