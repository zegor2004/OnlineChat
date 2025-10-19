using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat
{
    public class ChatModel
    {
        private ChatModel(Guid chatId, string userId, List<Message> messages)
        {
            ChatId = chatId;
            UserId = userId;
            Messages = messages;
        }
        public Guid ChatId { get; }
        public string UserId { get; } = string.Empty;
        List<Message> Messages { get; set; }

        public static ChatModel Create(string userId, List<Message> messages)
        {
            var guid = Guid.NewGuid();
            var chat = new ChatModel(guid, userId, messages);
            return chat;
        }
    }
}
