using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat
{
    public class MessageModel
    {
        private MessageModel(string userId, string text, DateTime createdAt)
        {
            UserId = userId;
            Text = text;
            CreatedAt = createdAt;
        }
        public string UserId { get; set; } = string.Empty;
        public string Text { get; set;} = string.Empty;
        public DateTime CreatedAt { get; set; }

        public static MessageModel Create(string userId, string text, DateTime createdAt)
        {
            var message = new MessageModel(userId, text, createdAt);
            return message;
        }

        public static MessageModel Create(string userId, string text)
        {
            var createdAt = DateTime.UtcNow;
            var message = new MessageModel(userId, text, createdAt);
            return message;
        }
    }
}
