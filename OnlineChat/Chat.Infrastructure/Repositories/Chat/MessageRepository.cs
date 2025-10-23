using Chat.Domain.Abstractions.Chat.Message;
using Chat.Domain.Models.Chat;
using Chat.Infrastructure.Entites.Chat;
using Chat.Infrastructure.Entites.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories.Chat
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

            var message =  MessageModel.Create(
                messagesEntity.user_id,
                messagesEntity.text,
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
                .Select(x => MessageModel.Create(x.user_id, x.text, x.created_at))
                .ToList();

            return messages;
        }

        public async Task Add(Guid chatId, Guid userId, string text, DateTime createdAt)
        {
            var messageEntity = new MessageEntity
            {
                chat_id = chatId,
                user_id = userId,
                text = text,
                created_at = createdAt
            };

            await _db.AddAsync(messageEntity);
            await _db.SaveChangesAsync();

        }
    }
}
