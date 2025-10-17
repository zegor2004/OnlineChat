using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;
using Chat.Infrastructure.Entites;
using Chat.Infrastructure.Entites.Chat;

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
                ChatId = guid,
                UserId = email_1,
            };

            var chat_2 = new ChatEntity
            {
                ChatId = guid,
                UserId = email_2,
            };

            await _db.AddAsync(chat_1);
            await _db.AddAsync(chat_2);
            await _db.SaveChangesAsync();

            return guid;
        }
    }
}
