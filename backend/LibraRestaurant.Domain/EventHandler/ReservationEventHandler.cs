using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Reservation;
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
    public sealed class ReservationEventHandler :
                INotificationHandler<ReservationDeletedEvent>,
                INotificationHandler<ReservationCreatedEvent>,
                INotificationHandler<ReservationUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public ReservationEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(ReservationDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Reservation>(notification.AggregateNumberId),
                cancellationToken);
        }

        public async Task Handle(ReservationUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Reservation>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
