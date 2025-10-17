using Chat.Domain.Abstractions.User;
using Chat.Domain.Models.User;
using Chat.Infrastructure.Entites;
using Chat.Infrastructure.Entites.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _db;
        public UserRepository(ChatDbContext db)
        {
            _db = db;
        }
        public async Task<List<ChatUser>> Get(string name)
        {
            var userEntites = await _db.users
                .Where(x => EF.Functions.Like(x.name, $"{name}%"))
                .AsNoTracking()
                .ToListAsync();

            var users = userEntites
                .Select(x => ChatUser.Create(x.email, string.Empty, x.name))
                .ToList();

            return users;
        }

        public async Task<string> Create(string email, string passwordHash, string name)
        {
            var userEntites = new UserEntity
            {
                email = email,
                password_hash = passwordHash,
                name = name,
                created_at = DateTime.UtcNow
            };
            await _db.AddAsync(userEntites);
            await _db.SaveChangesAsync();

            return userEntites.email;
        }

        public async Task<string> GetByEmail(string email)
        {
            var userEntity = await _db.users
                .FirstOrDefaultAsync(x => x.email == email);

            if (userEntity == null) return string.Empty;
            //var user = ChatUser.Create(userEntity.Email, userEntity.PasswordHash, userEntity.Name);
            return userEntity.password_hash;
        }

        public async Task<string> Update(string email, string passwordHash, string name)
        {
            await _db.users
                .Where(b => b.email == email)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.password_hash, b => passwordHash)
                .SetProperty(b => b.name, b => name)
                );

            return email;
        }

        public async Task<string> Delete(string email)
        {
            await _db.users
                .Where(b => b.email == email)
                .ExecuteDeleteAsync();

            return email;
        }
    }
}
