using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Models.Chat;

namespace Chat.Application.Services.Chat
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageService _messageServices;
        private readonly IUserService _userService;
        public ChatServices(IChatRepository chatRepository, IMessageService messageService, IUserService userService)
        {
            _chatRepository = chatRepository;
            _messageServices = messageService;
            _userService = userService;
        }
        public async Task<List<ChatViewModel>> GetChatPreview(string email)
        {
            var chats = await _chatRepository.GetChats(email);

            var chatsView = new List<ChatViewModel>();

            foreach (var chat in chats)
            {
                var message = await _messageServices.GetMessageLast(chat.ChatId);
                var user = await _userService.GetUserByEmail(chat.UserId);
                var chatView = ChatViewModel.Create(chat.ChatId, user, message);
                chatsView.Add(chatView);
            }
            return chatsView;
        }
        public async Task<ChatViewModel> GetChat(string email_1, string email_2)
        {
            var chatId = await _chatRepository.GetChat(email_1,email_2);

            if (chatId == Guid.Empty)
                return null;

            var messages = await _messageServices.GetMessages(chatId);

            var user = await _userService.GetUserByEmail(email_2);

            var chatView = ChatViewModel.Create(chatId, user, messages);

            return chatView;
        }

        public async Task<MessageModel> SendMessage(string email_1, string email_2, string text)
        {
            var chatId = await _chatRepository.GetChat(email_1, email_2);

            if (chatId == Guid.Empty) 
                chatId = await _chatRepository.Create(email_1, email_2);

            var message = await _messageServices.SendMessage(chatId, email_1, text);

            return message;
        }
    }
}
