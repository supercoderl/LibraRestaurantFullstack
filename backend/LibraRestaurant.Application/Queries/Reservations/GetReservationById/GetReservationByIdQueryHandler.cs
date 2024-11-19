using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reservations.GetReservationById;

public sealed class GetReservationByIdQueryHandler :
    IRequestHandler<GetReservationByIdQuery, ReservationViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryHandler(IReservationRepository reservationRepository, IMediatorHandler bus)
    {
        _reservationRepository = reservationRepository;
        _bus = bus;
    }

    public async Task<ReservationViewModel?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id);

        if (reservation is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetReservationByIdQuery),
                    $"Reservation with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ReservationViewModel.FromReservation(reservation, null);
    }
}