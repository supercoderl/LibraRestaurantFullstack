using System;

namespace LibraRestaurant.Shared.Events.Menu;

public sealed class MenuCreatedEvent : DomainEvent
{
    public MenuCreatedEvent(int menuId) : base(menuId)
    {
    }
}