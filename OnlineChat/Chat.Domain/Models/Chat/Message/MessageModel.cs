using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat.Message
{
    public class MessageModel
    {
        private MessageModel(Guid userId, string text, DateTime createdAt)
        {
            UserId = userId;
            Text = text;
            CreatedAt = createdAt;
        }
        public Guid UserId { get; }
        public string Text { get; } = string.Empty;
        public DateTime CreatedAt { get; }

        private const int MaxTextLenght = 500;
        public static MessageModel Create(Guid userId, string text, DateTime createdAt)
        {
            var message = new MessageModel(userId, text, createdAt);
            return message;
        }

        public static MessageModel Create(Guid userId, string text)
        {
            var createdAt = DateTime.UtcNow;
            var message = new MessageModel(userId, text, createdAt);
            return message;
        }

        public static MessageModel CreateEmpty()
        {
            var createdAt = DateTime.UtcNow;
            var message = new MessageModel(Guid.Empty, string.Empty, createdAt);
            return message;
        }
    }
}
