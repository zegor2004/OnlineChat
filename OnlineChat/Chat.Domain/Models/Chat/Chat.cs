using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat
{
    public class Chat
    {
        private Chat(Guid chatId, string userId)
        {
            ChatId = chatId;
            UserId = userId;
        }
        public int Id { get; }
        public Guid ChatId { get; }
        public string UserId { get; } = string.Empty;

        public static Chat Create(string userId)
        {
            var guid = Guid.NewGuid();
            var chat = new Chat(guid, userId);
            return chat;
        }
    }
}
