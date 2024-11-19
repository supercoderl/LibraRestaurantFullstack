using System;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Queries.Orders.GetOrderByStoreAndReservation;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reservations.GetReservationByTableNumberAndStoreId;

public sealed class GetReservationByTableNumberAndStoreIdQueryHandler :
    IRequestHandler<GetReservationByTableNumberAndStoreIdQuery, ReservationViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByTableNumberAndStoreIdQueryHandler(IReservationRepository reservationRepository, IMediatorHandler bus)
    {
        _reservationRepository = reservationRepository;
        _bus = bus;
    }

    public async Task<ReservationViewModel?> Handle(GetReservationByTableNumberAndStoreIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByReservationTableNumberAndStoreIdAsync(request.TableNumber, request.StoreId);

        if (reservation is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetReservationByTableNumberAndStoreIdQuery),
                    $"Reservation with table number {request.TableNumber} and store id {request.StoreId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        var orderId = await CheckOrderIsReady(request.StoreId, reservation.ReservationId);

        return ReservationViewModel.FromReservation(reservation, orderId);
    }

    private async Task<Guid?> CheckOrderIsReady(Guid storeId, int reservationId)
    {
        var order = await _bus.QueryAsync(new GetOrderByStoreAndReservationQuery(storeId, reservationId));
        if (order is not null) return order.OrderId;
        return null;
    }
}