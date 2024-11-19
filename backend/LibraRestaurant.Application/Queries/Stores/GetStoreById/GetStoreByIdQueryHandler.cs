using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Stores;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Stores.GetStoreById;

public sealed class GetStoreByIdQueryHandler :
    IRequestHandler<GetStoreByIdQuery, StoreViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IStoreRepository _storeRepository;

    public GetStoreByIdQueryHandler(IStoreRepository storeRepository, IMediatorHandler bus)
    {
        _storeRepository = storeRepository;
        _bus = bus;
    }

    public async Task<StoreViewModel?> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetByIdAsync(request.Id);

        if (store is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetStoreByIdQuery),
                    $"Store with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return StoreViewModel.FromMenu(store);
    }
}