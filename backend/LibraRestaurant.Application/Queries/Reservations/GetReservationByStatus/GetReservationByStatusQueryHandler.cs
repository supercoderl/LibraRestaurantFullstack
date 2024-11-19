using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reservations.GetReservationByStatus;

public sealed class GetReservationByStatusQueryHandler :
    IRequestHandler<GetReservationByStatusQuery, List<ReservationViewModel>>
{
    private readonly IMediatorHandler _bus;
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByStatusQueryHandler(IReservationRepository reservationRepository, IMediatorHandler bus)
    {
        _reservationRepository = reservationRepository;
        _bus = bus;
    }

    public async Task<List<ReservationViewModel>> Handle(GetReservationByStatusQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetByStatusAsync(request.Status);

        return reservations.Select(reservation => ReservationViewModel.FromReservation(reservation, null)).ToList();
    }
}