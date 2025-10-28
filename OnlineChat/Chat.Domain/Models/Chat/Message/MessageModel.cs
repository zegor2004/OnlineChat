using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat.Message
{
    public class MessageModel
    {
        private MessageModel(Guid messageId, Guid chatId, Guid userId, string text, bool isRead, DateTime createdAt)
        {
            MessageId = messageId;
            ChatId = chatId;
            UserId = userId;
            Text = text;
            IsRead = isRead;
            CreatedAt = createdAt;
        }
        public Guid MessageId { get;}
        public Guid ChatId { get;}
        public Guid UserId { get; }
        public string Text { get; } = string.Empty;
        public bool IsRead { get; }
        public DateTime CreatedAt { get; }

        private const int MaxTextLenght = 500;
        public static MessageModel Create(Guid messageId, Guid chatId, Guid userId, string text, bool isRead, DateTime createdAt)
        {
            var message = new MessageModel(messageId, chatId, userId, text, isRead, createdAt);
            return message;
        }

        public static MessageModel Create(Guid chatId, Guid userId, string text)
        {
            var message = new MessageModel(Guid.NewGuid(), chatId, userId, text, false, DateTime.UtcNow);
            return message;
        }

        public static MessageModel CreateEmpty()
        {
            var message = new MessageModel(Guid.Empty, Guid.Empty, Guid.Empty, string.Empty, false, DateTime.UtcNow);
            return message;
        }
    }
}
