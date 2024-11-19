using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Roles.GetRoleById;

public sealed class GetRoleByIdQueryHandler :
    IRequestHandler<GetRoleByIdQuery, RoleViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IRoleRepository _roleRepository;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMediatorHandler bus)
    {
        _roleRepository = roleRepository;
        _bus = bus;
    }

    public async Task<RoleViewModel?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);

        if (role is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetRoleByIdQuery),
                    $"Role with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return RoleViewModel.FromRole(role);
    }
}