using System;

namespace LibraRestaurant.Shared.Events.CategoryItem;

public sealed class CategoryItemCreatedEvent : DomainEvent
{
    public CategoryItemCreatedEvent(int categoryItemId) : base(categoryItemId)
    {
    }
}