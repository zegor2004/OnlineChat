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
            var userEntites = await _db.Users
                .Where(x => EF.Functions.Like(x.Name, $"{name}%"))
                .AsNoTracking()
                .ToListAsync();

            var users = userEntites
                .Select(x => ChatUser.Create(x.Email, string.Empty, x.Name))
                .ToList();

            return users;
        }

        public async Task<string> Create(string email, string passwordHash, string name)
        {
            var userEntites = new UserEntity
            {
                Email = email,
                PasswordHash = passwordHash,
                Name = name,
            };
            await _db.AddAsync(userEntites);
            await _db.SaveChangesAsync();

            return userEntites.Email;
        }

        public async Task<string> GetByEmail(string email)
        {
            var userEntity = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (userEntity == null) return string.Empty;
            //var user = ChatUser.Create(userEntity.Email, userEntity.PasswordHash, userEntity.Name);
            return userEntity.PasswordHash;
        }

        public async Task<string> Update(string email, string passwordHash, string name)
        {
            await _db.Users
                .Where(b => b.Email == email)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.PasswordHash, b => passwordHash)
                .SetProperty(b => b.Name, b => name)
                );

            return email;
        }

        public async Task<string> Delete(string email)
        {
            await _db.Users
                .Where(b => b.Email == email)
                .ExecuteDeleteAsync();

            return email;
        }
    }
}
