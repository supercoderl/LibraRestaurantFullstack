using System;

namespace LibraRestaurant.Shared.Events.CategoryItem;

public sealed class CategoryItemDeletedEvent : DomainEvent
{

    public CategoryItemDeletedEvent(int categoryItemId) : base(categoryItemId)
    {

    }
}