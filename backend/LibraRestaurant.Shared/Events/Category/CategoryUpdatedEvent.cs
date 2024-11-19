using System;

namespace LibraRestaurant.Shared.Events.Category;

public sealed class CategoryUpdatedEvent : DomainEvent
{

    public CategoryUpdatedEvent(int categoryId) : base(categoryId)
    {

    }
}