
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

namespace LibraRestaurant.Domain.Commands.Menus.DeleteMenu
{
    public sealed class DeleteMenuCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteMenuCommand>
    {
        private readonly IMenuRepository _menuRepository;

        public DeleteMenuCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMenuRepository menuRepository) : base(bus, unitOfWork, notifications)
        {
            _menuRepository = menuRepository;
        }

        public async Task Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
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

            _menuRepository.Remove(menu);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MenuDeletedEvent(request.MenuId));
            }
        }
    }
}
