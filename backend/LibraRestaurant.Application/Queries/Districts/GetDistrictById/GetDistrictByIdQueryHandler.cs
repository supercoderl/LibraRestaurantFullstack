using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Districts.GetDistrictById;

public sealed class GetDistrictByIdQueryHandler :
    IRequestHandler<GetDistrictByIdQuery, DistrictViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDistrictRepository _districtRepository;

    public GetDistrictByIdQueryHandler(IDistrictRepository districtRepository, IMediatorHandler bus)
    {
        _districtRepository = districtRepository;
        _bus = bus;
    }

    public async Task<DistrictViewModel?> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
    {
        var district = await _districtRepository.GetByIdAsync(request.Id);

        if (district is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDistrictByIdQuery),
                    $"District with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DistrictViewModel.FromDistrict(district);
    }
}