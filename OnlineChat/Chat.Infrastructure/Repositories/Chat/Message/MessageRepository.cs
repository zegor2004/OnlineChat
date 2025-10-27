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
        public async Task<MessageModel> GetMessageLast(Guid chatId)
        {
            var messagesEntity = await _db.messages
                .Where(x => x.chat_id == chatId)
                .OrderByDescending(x => x.created_at)
                .FirstOrDefaultAsync();

            var message = MessageModel.Create(
                messagesEntity.id,
                messagesEntity.user_id,
                messagesEntity.text,
                messagesEntity.is_read,
                messagesEntity.created_at);

            return message;
        }
        public async Task<List<MessageModel>> Get(Guid chatId)
        {
            var messagesEntity = await _db.messages
                .Where(x => x.chat_id == chatId)
                .AsNoTracking()
                .ToListAsync();

            var messages = messagesEntity
                .Select(x => MessageModel.Create(x.id, x.user_id, x.text, x.is_read, x.created_at))
                .ToList();

            return messages;
        }

        public async Task<bool> Add(Guid chatId, Guid userId, string text, DateTime createdAt)
        {
            var messageEntity = new MessageEntity
            {
                chat_id = chatId,
                user_id = userId,
                text = text,
                is_read = false,
                created_at = createdAt
            };

            await _db.AddAsync(messageEntity);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }
    }
}
