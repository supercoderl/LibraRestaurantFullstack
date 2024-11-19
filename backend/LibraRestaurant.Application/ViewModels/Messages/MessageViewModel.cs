using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Messages;

public sealed class MessageViewModel
{
    public int MessageId { get; set; }
    public string? SenderId { get; set; }
    public string? ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Time { get; set; }
    public bool IsRead { get; set; }
    public string? ConversationId { get; set; }
    public string MessageType { get; set; } = string.Empty;
    public string? AttachmentUrl { get; set; }

    public static MessageViewModel FromMessage(Message message)
    {
        return new MessageViewModel
        {
            MessageId = message.MessageId,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content,
            Time = message.Time,
            IsRead = message.IsRead,
            ConversationId = message.ConversationId,
            MessageType = message.MessageType,
            AttachmentUrl = message.AttachmentUrl,
        };
    }
}