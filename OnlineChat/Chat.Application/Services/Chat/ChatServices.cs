using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Models.Chat;

namespace Chat.Application.Services.Chat
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageService _messageServices;
        public ChatServices(IChatRepository chatRepository, IMessageService messageService)
        {
            _chatRepository = chatRepository;
            _messageServices = messageService;
        }
        public async Task<List<ChatViewModel>> GetChatPreview(string email)
        {
            var chatsId = await _chatRepository.GetChat(email);

            var chats = new List<ChatViewModel>();

            foreach (var chat in chatsId)
            {
                var message = await _messageServices.GetMessageLast(chat);
                var chat = ChatViewModel.Create()
            }
            
        }
        public async Task<Guid> GetChat(string email_1, string email_2)
        {
            var chatId = await _chatRepository.Get(email_1,email_2);
            if (chatId == Guid.Empty)
                chatId = await _chatRepository.Create(email_1, email_2);
            return chatId;
        }
    }
}
