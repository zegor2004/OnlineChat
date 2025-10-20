using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat
{
    public class MessageModel
    {
        private MessageModel(Guid chatId, string userId, string text, DateTime createdAt)
        {
            ChatId = chatId;
            UserId = userId;
            Text = text;
            CreatedAt = createdAt;
        }
        public Guid ChatId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Text { get; set;} = string.Empty;
        public DateTime CreatedAt { get; set; }

        public static MessageModel Create(Guid chatId, string userId, string text, DateTime createdAt)
        {
            var message = new MessageModel(chatId, userId, text, createdAt);
            return message;
        }

        public static MessageModel Create(Guid chatId, string userId, string text)
        {
            var createdAt = DateTime.UtcNow;
            var message = new MessageModel(chatId, userId, text, createdAt);
            return message;
        }
    }
}
