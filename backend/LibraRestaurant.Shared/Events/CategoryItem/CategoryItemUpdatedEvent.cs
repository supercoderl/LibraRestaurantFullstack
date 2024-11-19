using System;

namespace LibraRestaurant.Shared.Events.CategoryItem;

public sealed class CategoryItemUpdatedEvent : DomainEvent
{

    public CategoryItemUpdatedEvent(int categoryItemId) : base(categoryItemId)
    {

    }
}