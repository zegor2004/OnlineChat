using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Models.Chat;
using Chat.Infrastructure.Entites;
using Chat.Infrastructure.Entites.Chat;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories.Chat
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _db;
        public ChatRepository(ChatDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> Create(string email_1, string email_2)
        {
            var guid = Guid.NewGuid();
            var chat_1 = new ChatEntity
            {
                chat_id = guid,
                user_id = email_1,
            };

            var chat_2 = new ChatEntity
            {
                chat_id = guid,
                user_id = email_2,
            };

            await _db.AddAsync(chat_1);
            await _db.AddAsync(chat_2);
            await _db.SaveChangesAsync();

            return guid;
        }

        public async Task<List<ChatModel>> GetChats(string email)
        {

            var chatsEntity = await _db.chats
                .Where(c1 => c1.user_id == email)  
                .Join(_db.chats,                    
                c1 => c1.chat_id,               
                c2 => c2.chat_id,               
                (c1, c2) => new { c1, c2 })    
                .Where(x => x.c2.user_id != email)  
                .Select(x => x.c2)
                .AsNoTracking()
                .ToListAsync();

            var chats = chatsEntity
                .Select(x => ChatModel.Create(x.chat_id,x.user_id))
                .ToList();

            return chats;
        }

        public async Task<Guid> GetChat(string email_1, string email_2)
        {
            var chatEntity = await _db.chats
                .Where(x1 => x1.user_id == email_1)
                .Where(x1 => _db.chats.Any(x2 => x2.chat_id == x1.chat_id && x2.user_id == email_2))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            //var chat = ChatModel.Create(chatEntity.chat_id, chatEntity.user_id);
            if (chatEntity == null)
                return Guid.Empty;

            return chatEntity.chat_id;
        }
    }
}
