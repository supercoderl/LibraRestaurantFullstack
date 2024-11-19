using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.Messages;
using LibraRestaurant.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class MessagesContext : IMessagesContext
    {
        private readonly MessagesApi.MessagesApiClient _client;

        public MessagesContext(MessagesApi.MessagesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<MessageViewModel>> GetMessagesByIds(IEnumerable<int> ids)
        {
            var request = new GetMessagesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Messages.Select(message => new MessageViewModel(
                message.Id,
                message.SenderId,
                message.ReseiverId,
                message.Content,
                DateTime.Parse(message.Time),
                message.IsRead,
                message.ConversationId,
                message.MessageType,
                message.AttachmentUrl));
        }
    }
}
