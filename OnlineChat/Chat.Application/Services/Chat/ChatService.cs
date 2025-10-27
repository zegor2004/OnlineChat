using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Application.Services.User.Session;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Abstractions.Chat.Message;
using Chat.Domain.Abstractions.Hub;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Abstractions.User.Session;
using Chat.Domain.Models.Chat;
using Chat.Domain.Models.Chat.Message;
using Chat.Domain.Models.User;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Chat.Application.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageService _messageServices;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly IChatHubService _chatHubService;
        public ChatService(IChatRepository chatRepository, IMessageService messageService, IUserService userService, ISessionService sessionService, IChatHubService chatHubService)
        {
            _chatRepository = chatRepository;
            _messageServices = messageService;
            _userService = userService;
            _sessionService = sessionService;
            _chatHubService = chatHubService;
        }
        public async Task<List<ChatViewModel>> GetChatPreview(Guid UserId)
        {
            var chats = await _chatRepository.GetChats(UserId);

            var chatsView = new List<ChatViewModel>();

            foreach (var chat in chats)
            {
                var message = await _messageServices.GetMessageLast(chat.ChatId);
                var user = await _userService.GetUserByUserId(chat.UserId);
                var chatView = ChatViewModel.Create(chat.ChatId, user, message);
                chatsView.Add(chatView);
            }
            return chatsView;
        }
        public async Task<ChatViewModel> GetChat(Guid userIdFrom, Guid userIdTo)
        {
            var chatId = await _chatRepository.GetChat(userIdFrom, userIdTo);

            if (chatId == Guid.Empty)
                return ChatViewModel.CreateEmpty();
            
            var messages = await _messageServices.GetMessages(chatId);

            var user = await _userService.GetUserByUserId(userIdTo);

            var chatView = ChatViewModel.Create(chatId, user, messages);

            return chatView;
        }

        public async Task<MessageModel> SendMessage(Guid userIdFrom, Guid userIdTo, string text)
        {
            var chatId = await _chatRepository.GetChat(userIdFrom, userIdTo);

            if (chatId == Guid.Empty) 
                chatId = await _chatRepository.Create(userIdFrom, userIdTo);

            var message = await _messageServices.SendMessage(chatId, userIdFrom, text);

            //var sessions = await _sessionService.GetSessionUserByUserId(userIdTo);
            await _chatHubService.NotificationNewMessage(message.Text, userIdTo);

            return message;
        }
    }
}
