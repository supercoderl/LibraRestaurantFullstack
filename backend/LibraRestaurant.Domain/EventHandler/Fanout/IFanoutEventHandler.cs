using System.Threading.Tasks;
using LibraRestaurant.Shared.Events;

namespace LibraRestaurant.Domain.EventHandler.Fanout;

public interface IFanoutEventHandler
{
    Task<DomainEvent> HandleDomainEventAsync(DomainEvent @event);
}