using Chat.Infrastructure.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories.User
{
    public class SessionRepository
    {
        private readonly ChatDbContext _db;
        public SessionRepository(ChatDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Add(string connectionId, Guid userId)
        {
            var sessionEntity = new SessionEntity
            {
                connection_id = connectionId,
                user_id = userId
            };
            await _db.sessions
                .AddAsync(sessionEntity);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(string connectionId)
        {

        }
    }
}
