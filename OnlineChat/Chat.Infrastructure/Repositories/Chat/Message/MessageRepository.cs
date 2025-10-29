using Chat.Domain.Abstractions.Chat.Message;
using Chat.Domain.Models.Chat.Message;
using Chat.Infrastructure.Entites.Chat;
using Chat.Infrastructure.Entites.User;
using Chat.Infrastructure.Entities.Chat.Message;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories.Chat.Message
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _db;
        public MessageRepository(ChatDbContext db)
        {
            _db = db;
        }
        public async Task<MessageModel> GetLastMessage(Guid chatId)
        {
            var messageEntity = await _db.messages
                .Where(x => x.chat_id == chatId)
                .OrderByDescending(x => x.created_at)
                .FirstOrDefaultAsync();

            if (messageEntity == null)
                return MessageModel.CreateEmpty();

            var message = MessageModel.Create(
                messageEntity.id,
                messageEntity.chat_id,
                messageEntity.user_id,
                messageEntity.text,
                messageEntity.is_read,
                messageEntity.created_at);

            return message;
        }
        public async Task<List<MessageModel>> GetMessages(Guid chatId)
        {
            var messagesEntity = await _db.messages
                .Where(x => x.chat_id == chatId)
                .AsNoTracking()
                .ToListAsync();

            var messages = messagesEntity
                .Select(x => MessageModel.Create(x.id, x.chat_id, x.user_id, x.text, x.is_read, x.created_at))
                .ToList();

            return messages;
        }

        public async Task<bool> AddMessage(MessageModel message)
        {
            var messageEntity = new MessageEntity
            {
                id = message.MessageId,
                chat_id = message.ChatId,
                user_id = message.UserId,
                text = message.Text,
                is_read = false,
                created_at = message.CreatedAt
            };

            await _db.AddAsync(messageEntity);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateMessageStatus(Guid messageId)
        {
            var result = await _db.messages
                .Where(x => x.id == messageId)
                .ExecuteUpdateAsync(z => z
                .SetProperty(x => x.is_read, x => true));

            return result > 0;
        }

        public async Task<MessageModel> GetMessage(Guid messageId)
        {
            var messageEntity = await _db.messages
                .Where (x => x.id == messageId)
                .FirstOrDefaultAsync();

            var message = MessageModel.Create(
                messageEntity.id,
                messageEntity.chat_id,
                messageEntity.user_id,
                messageEntity.text,
                messageEntity.is_read,
                messageEntity.created_at
            );

            return message;
        }
    }
}
