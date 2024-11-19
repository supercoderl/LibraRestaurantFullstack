using System;

namespace LibraRestaurant.Shared.Events.Review;

public sealed class ReviewDeletedEvent : DomainEvent
{

    public ReviewDeletedEvent(int reviewId) : base(reviewId)
    {

    }
}