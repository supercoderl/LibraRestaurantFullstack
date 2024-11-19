using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Employee;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace LibraRestaurant.Domain.EventHandler;

public sealed class EmployeeEventHandler :
    INotificationHandler<EmployeeDeletedEvent>,
    INotificationHandler<EmployeeCreatedEvent>,
    INotificationHandler<EmployeeUpdatedEvent>,
    INotificationHandler<PasswordChangedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public EmployeeEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(PasswordChangedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(EmployeeDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Employee>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(EmployeeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Employee>(notification.AggregateId),
            cancellationToken);
    }
}