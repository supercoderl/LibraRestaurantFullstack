using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands;
using LibraRestaurant.Domain.DomainEvents;
using LibraRestaurant.Domain.EventHandler.Fanout;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Shared.Events;
using MediatR;

namespace LibraRestaurant.Infrastructure;

public sealed class InMemoryBus : IMediatorHandler
{
    private readonly IDomainEventStore _domainEventStore;
    private readonly IFanoutEventHandler _fanoutEventHandler;
    private readonly IMediator _mediator;

    public InMemoryBus(
        IMediator mediator,
        IDomainEventStore domainEventStore,
        IFanoutEventHandler fanoutEventHandler)
    {
        _mediator = mediator;
        _domainEventStore = domainEventStore;
        _fanoutEventHandler = fanoutEventHandler;
    }

    public Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query)
    {
        return _mediator.Send(query);
    }

    public async Task RaiseEventAsync<T>(T @event) where T : DomainEvent
    {
        await _domainEventStore.SaveAsync(@event);

        await _mediator.Publish(@event);

        await _fanoutEventHandler.HandleDomainEventAsync(@event);
    }

    public Task SendCommandAsync<T>(T command) where T : CommandBase
    {
        return _mediator.Send(command);
    }
}