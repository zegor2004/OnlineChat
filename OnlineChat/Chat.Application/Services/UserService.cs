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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _usersRerositry = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<ChatUser>> GetAllUsers()
        {
            return await _usersRerositry.Get();
        }

        public async Task<string> Registration(string email, string password, string name)
        {
            var mes = ChatUser.DataValidation(email, password, name);
            if (!string.IsNullOrEmpty(mes)) return mes;

            var _user = await _usersRerositry.GetByEmail(email);
            if (!string.IsNullOrEmpty(_user)) return "This email is already busy";

            var passwordHash = _passwordHasher.Generate(password);

            var emails = await _usersRerositry.Create(email, passwordHash, name);

            return string.Empty;
        }

        public async Task<string> Login(string email, string password)
        {
            string passwordHash = await _usersRerositry.GetByEmail(email);
            if (string.IsNullOrEmpty(passwordHash) || !_passwordHasher.Verify(password, passwordHash)) 
                return string.Empty;

            var token = _jwtProvider.GenerateToken(email);

            return token;
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
