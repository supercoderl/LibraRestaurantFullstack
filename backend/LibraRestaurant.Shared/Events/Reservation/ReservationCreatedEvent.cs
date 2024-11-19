using System;

namespace LibraRestaurant.Shared.Events.Reservation;

public sealed class ReservationCreatedEvent : DomainEvent
{
    public ReservationCreatedEvent(int reservationId) : base(reservationId)
    {
    }
}