using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Messages
{
    public sealed record UpdateMessageViewModel(
        int MessageId,
        string? SenderId,
        string? ReceiverId,
        string Content,
        DateTime Time,
        bool IsRead,
        string? ConversationId,
        string MessageType,
        string? AttachmentUrl);
}
