using Chat.Domain.Abstractions.User.Session;
using Chat.Infrastructure.Entities.User;
using Chat.Infrastructure.Entities.User.Session;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories.User.Session
{
    public class SessionRepository : ISessionRepository
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
                user_id = userId,
                created_at = DateTime.UtcNow
            };
            await _db.sessions
                .AddAsync(sessionEntity);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(string connectionId)
        {
            var result = await _db.sessions
                .Where(x => x.connection_id == connectionId)
                .ExecuteDeleteAsync();

            return result > 0;
        }

        public async Task<List<string>> Get(Guid userId)
        {
            var sessionEntity = await _db.sessions
                .Where(x => x.user_id == userId)
                .ToListAsync();

            var connectionsList = sessionEntity
                .Select(x => x.connection_id)
                .ToList();
            
            return connectionsList;
        }
    }
}
