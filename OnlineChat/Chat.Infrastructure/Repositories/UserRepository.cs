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
                .Select(x => ChatUser.Create(x.Email, x.Password, x.Name).User)
                .ToList();

            return users;
        }

        public async Task<string> Create(ChatUser user)
        {
            var userEntites = new UserEntity
            {
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
            };
            await _db.AddAsync(userEntites);
            await _db.SaveChangesAsync();

            return userEntites.Email;
        }

        public async Task<string> Update(string email, string password, string name)
        {
            await _db.Users
                .Where(b => b.Email == email)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Password, b => password)
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
