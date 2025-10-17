using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;

namespace Chat.Application.Services.Chat
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepository _chatRepository;
        public ChatServices(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        public async Task<Guid> SendMessage(string email_1, string email_2)
        {
            var chatId = _chatRepository.Get(email_1,email_2);
            if (chatId == null)
                chatId = _chatRepository.Create(email_1, email_2);

            return new Guid();
        }
    }
}
