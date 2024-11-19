using System;

namespace LibraRestaurant.Shared.Events.Role;

public sealed class ReviewUpdatedEvent : DomainEvent
{

    public ReviewUpdatedEvent(int reviewId) : base(reviewId)
    {

    }
}