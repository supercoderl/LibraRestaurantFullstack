using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands;
using LibraRestaurant.Shared.Events;
using MediatR;

namespace LibraRestaurant.Domain.Interfaces;

public interface IMediatorHandler
{
    Task RaiseEventAsync<T>(T @event) where T : DomainEvent;

    Task SendCommandAsync<T>(T command) where T : CommandBase;

    Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query);
}