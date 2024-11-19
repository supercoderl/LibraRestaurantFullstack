
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Role;

namespace LibraRestaurant.Domain.Commands.Roles.CreateRole
{
    public sealed class CreateRoleCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IRoleRepository roleRepository) : base(bus, unitOfWork, notifications)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var role = new Entities.Role(
                request.RoleId,
                request.Name,
                request.Description);

            _roleRepository.Add(role);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new RoleCreatedEvent(role.RoleId));
            }
        }
    }
}
