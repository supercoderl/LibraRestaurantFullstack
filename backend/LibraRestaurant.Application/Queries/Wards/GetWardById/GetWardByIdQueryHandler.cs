using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Wards;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Wards.GetWardById;

public sealed class GetWardByIdQueryHandler :
    IRequestHandler<GetWardByIdQuery, WardViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IWardRepository _wardRepository;

    public GetWardByIdQueryHandler(IWardRepository wardRepository, IMediatorHandler bus)
    {
        _wardRepository = wardRepository;
        _bus = bus;
    }

    public async Task<WardViewModel?> Handle(GetWardByIdQuery request, CancellationToken cancellationToken)
    {
        var ward = await _wardRepository.GetByIdAsync(request.Id);

        if (ward is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetWardByIdQuery),
                    $"Ward with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return WardViewModel.FromWard(ward);
    }
}