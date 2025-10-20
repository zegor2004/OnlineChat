using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Chat
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<List<MessageModel>> GetMessages(Guid chat_id)
        {
            var messages = await _messageRepository.Get(chat_id);

            return messages;
        }
        public async Task<MessageModel> GetMessageLast(Guid chat_id)
        {
            var message = await _messageRepository.GetMessageLast(chat_id);

            return message;
        }
    }
}
