using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat
{
    public class ChatModel
    {
        private ChatModel(Guid chatId, string userId)
        {
            ChatId = chatId;
            UserId = userId;
        }
        public Guid ChatId { get; }
        public string UserId { get; } = string.Empty;

        public static ChatModel Create(Guid chatId, string userId)
        {
            var chat = new ChatModel(chatId, userId);
            return chat;
        }
    }
}
