using System;

namespace LibraRestaurant.Shared.Events.Category;

public sealed class CategoryCreatedEvent : DomainEvent
{
    public CategoryCreatedEvent(int categoryId) : base(categoryId)
    {
    }
}