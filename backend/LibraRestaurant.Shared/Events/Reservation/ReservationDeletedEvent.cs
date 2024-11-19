using System;

namespace LibraRestaurant.Shared.Events.Reservation;

public sealed class ReservationDeletedEvent : DomainEvent
{

    public ReservationDeletedEvent(int reservationId) : base(reservationId)
    {

    }
}