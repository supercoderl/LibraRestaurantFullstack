
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
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Store;

namespace LibraRestaurant.Domain.Commands.Stores.UpdateStore
{
    public sealed class UpdateStoreCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateStoreCommand>
    {
        private readonly IStoreRepository _storeRepository;

        public UpdateStoreCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IStoreRepository storeRepository) : base(bus, unitOfWork, notifications)
        {
            _storeRepository = storeRepository;
        }

        public async Task Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
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

            store.SetName(request.Name);
            store.SetCityId(request.CityId);
            store.SetDisctrictId(request.DistrictId);
            store.SetWardId(request.WardId);
            store.SetActive(request.IsActive);
            store.SetTaxCode(request.TaxCode);
            store.SetAddress(request.Address);
            store.SetGpsLocation(request.GpsLocation);
            store.SetPostalCode(request.PostalCode);
            store.SetPhone(request.Phone);
            store.SetFax(request.Fax);
            store.SetEmail(request.Email);
            store.SetWebsite(request.Website);
            store.SetLogo(request.Logo);
            store.SetBankBranch(request.BankBranch);
            store.SetBankCode(request.BankCode);
            store.SetBankAccount(request.BankAccount);

            _storeRepository.Update(store);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new StoreUpdatedEvent(store.StoreId));
            }
        }
    }
}
