using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Messages;
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
    public sealed class MessageEventHandler :
        INotificationHandler<MessageSentEvent>,
        INotificationHandler<MessageUpdatedEvent>    
    {
        private readonly IDistributedCache _distributedCache;

        public MessageEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(MessageSentEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(MessageUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<Message>(notification.AggregateNumberId),
                cancellationToken);
        }
    }
}
