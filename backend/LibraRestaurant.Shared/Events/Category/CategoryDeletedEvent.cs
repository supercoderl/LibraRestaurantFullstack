using System;

namespace LibraRestaurant.Shared.Events.Category;

public sealed class CategoryDeletedEvent : DomainEvent
{

    public CategoryDeletedEvent(int categoryId) : base(categoryId)
    {

    }
}