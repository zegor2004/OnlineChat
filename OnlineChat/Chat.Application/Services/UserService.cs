using Chat.Domain.Abstractions;
using Chat.Domain.Models;
using Chat.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRerositry;

        public UserService(IUserRepository usersRepository)
        {
            _usersRerositry = usersRepository;
        }

        public async Task<List<ChatUser>> GetAllUsers()
        {
            return await _usersRerositry.Get();
        }

        public async Task<string> CreateUser(ChatUser user)
        {
            return await _usersRerositry.Create(user);
        }

        public async Task<string> Login(ChatUser user)
        {
            string passwordHash = await _usersRerositry.GetUserByEmail(user.Email);
            return passwordHash;
        }

        public async Task<string> UpdateUser(string email, string passwordHash, string name)
        {
            return await _usersRerositry.Update(email, passwordHash, name);
        }

        public async Task<string> DeleteUser(string email)
        {
            return await _usersRerositry.Delete(email);
        }
    }
}
