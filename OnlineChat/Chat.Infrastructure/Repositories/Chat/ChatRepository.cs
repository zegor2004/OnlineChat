using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;
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

        public async Task<Guid> Get(string email_1, string email_2)
        {
            var chatId = await _db.chats
                .Where(x1 => x1.user_id == email_1)
                .Where(x1 => _db.chats.Any(x2 => x2.chat_id == x1.chat_id && x2.user_id == email_2))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return chatId.chat_id;
        }
    }
}
