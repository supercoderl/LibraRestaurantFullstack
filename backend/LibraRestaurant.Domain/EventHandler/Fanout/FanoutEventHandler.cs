using System.Threading.Tasks;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Rabbitmq;
using LibraRestaurant.Shared.Events;

namespace LibraRestaurant.Domain.EventHandler.Fanout;

public sealed class FanoutEventHandler : IFanoutEventHandler
{
    private readonly RabbitMqHandler _rabbitMqHandler;

    public FanoutEventHandler(
        RabbitMqHandler rabbitMqHandler)
    {
        _rabbitMqHandler = rabbitMqHandler;
        _rabbitMqHandler.InitializeExchange(Messaging.ExchangeNameNotifications);
    }

    public Task<DomainEvent> HandleDomainEventAsync(DomainEvent @event)
    {
        _rabbitMqHandler.EnqueueExchangeMessage(
            Messaging.ExchangeNameNotifications,
            @event);

        return Task.FromResult(@event);
    }
}