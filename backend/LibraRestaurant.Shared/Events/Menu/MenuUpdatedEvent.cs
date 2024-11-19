using System;

namespace LibraRestaurant.Shared.Events.Menu;

public sealed class MenuUpdatedEvent : DomainEvent
{

    public MenuUpdatedEvent(int menuId) : base(menuId)
    {

    }
}