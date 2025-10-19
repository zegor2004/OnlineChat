using Chat.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Chat
{
    public class ChatViewModel
    {
        private ChatViewModel(Guid chatId, UserModel user, List<MessageModel> messages)
        {
            ChatId = chatId;
            User = user;
            Messages = messages;
        }
        public Guid ChatId { get; }
        public UserModel User { get; }
        List<MessageModel> Messages { get; }

        public static ChatViewModel Create(UserModel user, List<MessageModel> messages)
        {
            var guid = Guid.NewGuid();
            var chat = new ChatViewModel(guid, user, messages);
            return chat;
        }
    }
}
