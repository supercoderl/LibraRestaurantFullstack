using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Shared.Events.Messages
{
    public sealed class MessageUpdatedEvent : DomainEvent
    {
        public MessageUpdatedEvent(int messageId) : base(messageId)
        {
        }
    }
}
