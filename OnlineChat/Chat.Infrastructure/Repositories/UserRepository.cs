using Chat.Domain.Abstractions;
using Chat.Domain.Models;
using Chat.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _db;
        public UserRepository(ChatDbContext db)
        {
            _db = db;
        }

        public async Task<List<ChatUser>> Get()
        {
            var userEntites = await _db.Users
                .AsNoTracking()
                .ToListAsync();

            var users = userEntites
                .Select(x => ChatUser.Create(x.Email, x.PasswordHash, x.Name))
                .ToList();

            return users;
        }

        public async Task<string> Create(ChatUser user)
        {
            var userEntites = new UserEntity
            {
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Name = user.Name,
            };
            await _db.AddAsync(userEntites);
            await _db.SaveChangesAsync();

            return userEntites.Email;
        }

        public async Task<string> GetUserByEmail(string email)
        {
            var userEntity = await _db.Users
                .FirstAsync(x => x.Email == email);

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
