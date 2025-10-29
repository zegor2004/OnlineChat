using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Abstractions.Chat.Message;
using Chat.Domain.Abstractions.Hub;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Models.Chat;
using Chat.Domain.Models.Chat.Message;

namespace Chat.Application.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageService _messageServices;
        private readonly IUserService _userService;
        private readonly IChatHubService _chatHubService;
        public ChatService(IChatRepository chatRepository, IMessageService messageService, IUserService userService, IChatHubService chatHubService)
        {
            _chatRepository = chatRepository;
            _messageServices = messageService;
            _userService = userService;
            _chatHubService = chatHubService;
        }
        public async Task<List<ChatViewModel>> GetChatsPreview(Guid UserId)
        {
            var chats = await _chatRepository.GetChats(UserId);

            var chatsView = new List<ChatViewModel>();

            foreach (var chat in chats)
            {
                var message = await _messageServices.GetLastMessage(chat.ChatId);
                var user = await _userService.GetUserByUserId(chat.UserId);
                var chatView = ChatViewModel.Create(chat.ChatId, user, message);
                chatsView.Add(chatView);
            }
            return chatsView;
        }
        public async Task<ChatViewModel> GetChat(Guid chatId, Guid userIdFrom)
        {            
            var messages = await _messageServices.GetMessages(chatId);

            var userIdTo = await _chatRepository.GetUserIdFromChat(chatId, userIdFrom);

            var user = await _userService.GetUserByUserId(userIdTo);

            var chatView = ChatViewModel.Create(chatId, user, messages);

            return chatView;
        }

        public async Task<Guid> GetChatId(Guid userIdFrom, Guid userIdTo)
        {
            var chatId = await _chatRepository.GetChatId(userIdFrom, userIdTo);

            if (chatId == Guid.Empty)
                chatId = await _chatRepository.Create(userIdFrom, userIdTo);

            return chatId;
        }

        public async Task<MessageModel> SendMessage(Guid userIdFrom, Guid chatId, string text)
        {
            var userIdTo = await _chatRepository.GetUserIdFromChat(chatId, userIdFrom);

            var message = await _messageServices.SendMessage(chatId, userIdFrom, text);

            await _chatHubService.NotificationNewMessage(message, userIdTo);

            return message;
        }

        public async Task<bool> UpdateMessageStatus(Guid messageId)
        {
            var result = await _messageServices.UpdateMessageStatus(messageId);
            if (result)
            {
                var message = await _messageServices.GetMessage(messageId);
                await _chatHubService.UpdateMessageStatus(message);
            }

            return result;
        }
    }
}
