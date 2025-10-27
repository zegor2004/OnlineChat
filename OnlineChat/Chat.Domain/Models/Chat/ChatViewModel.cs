using Chat.Domain.Models.Chat.Message;
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
        private ChatViewModel(Guid chatId, UserViewModel user, List<MessageModel> messages)
        {
            ChatId = chatId;
            User = user;
            Messages = messages;
        }
        public Guid ChatId { get; }
        public UserViewModel User { get; }
        public List<MessageModel> Messages { get; }

        public static ChatViewModel Create(Guid guid, UserViewModel user, List<MessageModel> messages)
        {
            //var guid = Guid.NewGuid();
            var chat = new ChatViewModel(guid, user, messages);
            return chat;
        }

        public static ChatViewModel Create(Guid guid, UserViewModel user, MessageModel message)
        {
            //var guid = Guid.NewGuid();
            var messages = new List<MessageModel>();
            messages.Add(message);
            var chat = new ChatViewModel(guid, user, messages);
            return chat;
        }

        public static ChatViewModel CreateEmpty()
        {
            var messageEmpty = new List<MessageModel>();

            var userEmpty = UserViewModel.Create(Guid.Empty, string.Empty);

            var chat = new ChatViewModel(Guid.Empty, userEmpty, messageEmpty);
            
            return chat;
        }
    }
}
