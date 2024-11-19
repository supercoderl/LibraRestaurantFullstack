
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Store;

namespace LibraRestaurant.Domain.Commands.Stores.CreateStore
{
    public sealed class CreateStoreCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateStoreCommand>
    {
        private readonly IStoreRepository _storeRepository;

        public CreateStoreCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IStoreRepository storeRepository) : base(bus, unitOfWork, notifications)
        {
            _storeRepository = storeRepository;
        }

        public async Task Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var store = new Entities.Store(
                request.StoreId,
                request.Name,
                request.CityId,
                request.DistrictId,
                request.WardId,
                request.IsActive,
                request.TaxCode,
                request.Address,
                request.GpsLocation,
                request.PostalCode,
                request.Phone,
                request.Fax,
                request.Email,
                request.Website,
                request.Logo,
                request.BankBranch,
                request.BankCode,
                request.BankAccount);

            _storeRepository.Add(store);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new StoreCreatedEvent(store.StoreId));
            }
        }
    }
}
