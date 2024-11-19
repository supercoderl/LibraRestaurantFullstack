using System;

namespace LibraRestaurant.Shared.Events.Review;

public sealed class ReviewCreatedEvent : DomainEvent
{
    public ReviewCreatedEvent(int reviewId) : base(reviewId)
    {
    }
}