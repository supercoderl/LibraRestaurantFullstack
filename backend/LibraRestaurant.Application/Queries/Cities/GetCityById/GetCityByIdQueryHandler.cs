using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Cities.GetCityById;

public sealed class GetCityByIdQueryHandler :
    IRequestHandler<GetCityByIdQuery, CityViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICityRepository _cityRepository;

    public GetCityByIdQueryHandler(ICityRepository cityRepository, IMediatorHandler bus)
    {
        _cityRepository = cityRepository;
        _bus = bus;
    }

    public async Task<CityViewModel?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id);

        if (city is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCityByIdQuery),
                    $"City with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CityViewModel.FromCity(city);
    }
}