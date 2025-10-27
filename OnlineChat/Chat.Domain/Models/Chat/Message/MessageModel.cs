using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat.Message
{
    public class MessageModel
    {
        private MessageModel(int id, Guid userId, string text, bool isRead, DateTime createdAt)
        {
            UserId = userId;
            Text = text;
            IsRead = isRead;
            CreatedAt = createdAt;
        }
        public int Id { get;}
        public Guid UserId { get; }
        public string Text { get; } = string.Empty;
        public bool IsRead { get; }
        public DateTime CreatedAt { get; }

        private const int MaxTextLenght = 500;
        public static MessageModel Create(int id, Guid userId, string text, bool isRead, DateTime createdAt)
        {
            var message = new MessageModel(id, userId, text, isRead, createdAt);
            return message;
        }

        public static MessageModel Create(Guid userId, string text)
        {
            var createdAt = DateTime.UtcNow;
            var message = new MessageModel(0, userId, text, false, createdAt);
            return message;
        }

        public static MessageModel CreateEmpty()
        {
            var createdAt = DateTime.UtcNow;
            var message = new MessageModel(0, Guid.Empty, string.Empty, false, createdAt);
            return message;
        }
    }
}
