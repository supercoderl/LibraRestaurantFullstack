using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.Events.OrderHead
{
    public class OrderUpdatedEvent : DomainEvent
    {
        public OrderUpdatedEvent(Guid orderId) : base(orderId)
        {

        }
    }
}
