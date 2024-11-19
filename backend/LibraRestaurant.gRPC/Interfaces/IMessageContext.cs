using LibraRestaurant.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IMessagesContext
    {
        Task<IEnumerable<MessageViewModel>> GetMessagesByIds(IEnumerable<int> ids);
    }
}
