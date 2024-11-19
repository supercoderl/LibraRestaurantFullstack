using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Token;
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
    public sealed class TokenEventHandler :
                INotificationHandler<TokenCreatedEvent>,
                INotificationHandler<TokenUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public TokenEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(TokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(TokenUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Token>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
