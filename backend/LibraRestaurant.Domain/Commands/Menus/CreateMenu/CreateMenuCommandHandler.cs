
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;

namespace LibraRestaurant.Domain.Commands.Menus.CreateMenu
{
    public sealed class CreateMenuCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateMenuCommand>
    {
        private readonly IMenuRepository _menuRepository;

        public CreateMenuCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMenuRepository menuRepository) : base(bus, unitOfWork, notifications)
        {
            _menuRepository = menuRepository;
        }

        public async Task Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var menu = new Entities.Menu(
                request.MenuId,
                request.StoreId,
                request.Name,
                request.Description,
                request.IsActive);

            _menuRepository.Add(menu);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MenuCreatedEvent(menu.MenuId));
            }
        }
    }
}
