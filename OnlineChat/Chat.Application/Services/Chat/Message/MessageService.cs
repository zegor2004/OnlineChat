using Chat.Domain.Abstractions.Chat.Message;
using Chat.Domain.Models.Chat.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Chat.Message
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

        public async Task<MessageModel> SendMessage(Guid chatId, Guid UserId, string text)
        {
            var message = MessageModel.Create(UserId, text);

            await _messageRepository.Add(chatId, UserId, text, message.CreatedAt);

            return message;
        }
    }
}
