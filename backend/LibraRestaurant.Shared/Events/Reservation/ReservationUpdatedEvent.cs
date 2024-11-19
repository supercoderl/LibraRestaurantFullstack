using System;

namespace LibraRestaurant.Shared.Events.Reservation;

public sealed class ReservationUpdatedEvent : DomainEvent
{

    public ReservationUpdatedEvent(int reservationId) : base(reservationId)
    {

    }
}