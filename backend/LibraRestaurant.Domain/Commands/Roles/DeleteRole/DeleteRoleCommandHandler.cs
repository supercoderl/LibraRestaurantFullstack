
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
using LibraRestaurant.Shared.Events.Role;

namespace LibraRestaurant.Domain.Commands.Roles.DeleteRole
{
    public sealed class DeleteRoleCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IRoleRepository roleRepository) : base(bus, unitOfWork, notifications)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var role = await _roleRepository.GetByIdAsync(request.RoleId);

            if (role is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no role with Id {request.RoleId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _roleRepository.Remove(role);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new RoleDeletedEvent(request.RoleId));
            }
        }
    }
}
