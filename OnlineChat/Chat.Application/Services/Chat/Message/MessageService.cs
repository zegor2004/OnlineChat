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
        public async Task<List<MessageModel>> GetMessages(Guid chatId)
        {
            var messages = await _messageRepository.GetMessages(chatId);

            return messages;
        }
        public async Task<MessageModel> GetLastMessage(Guid chat_id)
        {
            var message = await _messageRepository.GetLastMessage(chat_id);

            return message;
        }

        public async Task<MessageModel> SendMessage(Guid chatId, Guid UserId, string text)
        {
            var message = MessageModel.Create(chatId, UserId, text);

            await _messageRepository.AddMessage(message);

            return message;
        }

        public async Task<bool> UpdateMessageStatus(Guid messageId)
        {
            var result = await _messageRepository.UpdateMessageStatus(messageId);

            return result;
        }

        public async Task<MessageModel> GetMessage(Guid messageId)
        {
            var message = await _messageRepository.GetMessage(messageId);

            return message;
        }
    }
}
