using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Messages.UpdateMessage
{
    public sealed class UpdateMessageCommand : CommandBase
    {
        private static readonly UpdateMessageCommandValidation s_validation = new();

        public int MessageId { get; }
        public string? SenderId { get; }
        public string? ReceiverId { get; }
        public string Content { get; }
        public DateTime Time { get; }
        public bool IsRead { get; }
        public string? ConversationId { get; }
        public string MessageType { get; }
        public string? AttachmentUrl { get; }

        public UpdateMessageCommand(
            int messageId,
            string? senderId,
            string? receiverId,
            string content,
            DateTime time,
            bool isRead,
            string? conversationId,
            string messageType,
            string? attachmentUrl) : base(messageId)
        {
            MessageId = messageId;
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
            Time = time;
            IsRead = isRead;
            ConversationId = conversationId;
            MessageType = messageType;
            AttachmentUrl = attachmentUrl;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
