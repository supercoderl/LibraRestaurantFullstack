using System.Threading.Tasks;
using LibraRestaurant.Shared.Events;

namespace LibraRestaurant.Domain.DomainEvents;

public interface IDomainEventStore
{
    Task SaveAsync<T>(T domainEvent) where T : DomainEvent;
}