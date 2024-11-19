using System;

namespace LibraRestaurant.Shared.Events.Menu;

public sealed class MenuDeletedEvent : DomainEvent
{

    public MenuDeletedEvent(int menuId) : base(menuId)
    {

    }
}