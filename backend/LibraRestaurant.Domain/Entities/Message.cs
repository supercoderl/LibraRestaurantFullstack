using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Message : Entity
    {
        public int MessageId { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }
        public string? ConversationId { get; set; }
        public string MessageType { get; set; }
        public string? AttachmentUrl { get; set; }

        public Message(
            int messageId,
            string? senderId,
            string? receiverId,
            string content,
            DateTime time,
            bool isRead,
            string? conversationId,
            string messageType,
            string? attachmentUrl
        ) : base(messageId)
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

        public void SetSender(string? senderId)
        {
            SenderId = senderId;
        }

        public void SetReceiver(string? receiverId)
        {
            ReceiverId = receiverId;
        }

        public void SetContent(string content)
        {
            Content = content;
        }

        public void SetTime(DateTime time)
        {
            Time = time;
        }

        public void SetIsRead(bool isRead)
        {
            IsRead = isRead;
        }

        public void SetConversation(string? conversationId)
        {
            ConversationId = conversationId;
        }

        public void SetMessageType(string messageType)
        {
            MessageType = messageType;
        }

        public void SetAttachmentUrl(string? attachmentUrl)
        {
            AttachmentUrl = attachmentUrl;
        }
    }
}
