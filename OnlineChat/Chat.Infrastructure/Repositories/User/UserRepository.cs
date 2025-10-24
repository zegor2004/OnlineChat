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

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var userEntity = await _db.users
                .Where(x => x.email == email)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (userEntity == null) return UserModel.CreateEmpty();
            var user = UserModel.Create(userEntity.id, userEntity.email, userEntity.password_hash, userEntity.name);
            
            return user;
        }

        public async Task<UserViewModel> GetUserByUserId(Guid userId)
        {
            var userEntity = await _db.users
                .Where(x => x.id == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var user = UserViewModel.Create(userEntity.id, userEntity.name);

            return user;
        }
        public async Task<List<UserViewModel>> GetUsersByName(Guid userId, string name)
        {
            var userEntites = await _db.users
                .Where(x => EF.Functions.Like(x.name, $"{name}%") && x.id != userId)
                .AsNoTracking()
                .ToListAsync();

            var users = userEntites
                .Select(x => UserViewModel.Create(x.id, x.name))
                .ToList();

            return users;
        }

        public async Task<bool> Create(Guid id, string email, string passwordHash, string name)
        {
            var userEntites = new UserEntity
            {
                id = id,
                email = email,
                password_hash = passwordHash,
                name = name,
                created_at = DateTime.UtcNow
            };
            await _db.AddAsync(userEntites);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> GetUserBusy(string email)
        {
            var userEntity = await _db.users
                .FirstOrDefaultAsync(x => x.email == email);

            if (userEntity == null) return false;
            //var user = ChatUser.Create(userEntity.Email, userEntity.PasswordHash, userEntity.Name);
            return true;
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
