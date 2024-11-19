
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Store;

namespace LibraRestaurant.Domain.Commands.Stores.DeleteStore
{
    public sealed class DeleteStoreCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteStoreCommand>
    {
        private readonly IStoreRepository _storeRepository;

        public DeleteStoreCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IStoreRepository storeRepository) : base(bus, unitOfWork, notifications)
        {
            _storeRepository = storeRepository;
        }

        public async Task Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no store with Id {request.StoreId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _storeRepository.Remove(store);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new StoreDeletedEvent(request.StoreId));
            }
        }
    }
}
