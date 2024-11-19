using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Menus.GetMenuById;

public sealed class GetMenuByIdQueryHandler :
    IRequestHandler<GetMenuByIdQuery, MenuViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IMenuRepository _menuRepository;

    public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMediatorHandler bus)
    {
        _menuRepository = menuRepository;
        _bus = bus;
    }

    public async Task<MenuViewModel?> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.Id);

        if (menu is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetMenuByIdQuery),
                    $"Menu with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return MenuViewModel.FromMenu(menu);
    }
}