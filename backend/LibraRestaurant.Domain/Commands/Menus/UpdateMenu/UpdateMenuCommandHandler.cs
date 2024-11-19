
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

namespace LibraRestaurant.Domain.Commands.Menus.UpdateMenu
{
    public sealed class UpdateMenuCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateMenuCommand>
    {
        private readonly IMenuRepository _menuRepository;

        public UpdateMenuCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMenuRepository menuRepository) : base(bus, unitOfWork, notifications)
        {
            _menuRepository = menuRepository;
        }

        public async Task Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var menu = await _menuRepository.GetByIdAsync(request.MenuId);

            if (menu is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no menu with Id {request.MenuId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            menu.SetName(request.Name);
            menu.SetStoreId(request.StoreId);
            menu.SetDescription(request.Description);
            menu.SetActive(request.IsActive);

            _menuRepository.Update(menu);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MenuUpdatedEvent(menu.MenuId));
            }
        }
    }
}
