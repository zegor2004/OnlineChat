using Chat.Domain.Abstractions.Auth;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Abstractions.User.Session;
using Chat.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRerositry;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly ISessionService _sessionService;

        public UserService(IUserRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, ISessionService sessionService)
        {
            _usersRerositry = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _sessionService = sessionService;
        }
        public async Task<UserViewModel> GetUserByUserId(Guid userId)
        {
            var user = await _usersRerositry.GetUserByUserId(userId);
            var session = await _sessionService.GetUserSessions(user.UserId);
            var isOnline = session.Count() > 0 ? true : false;
            user.UpdateStatus(isOnline);
            return user;
        }
        public async Task<List<UserViewModel>> FindUserByName(Guid userId, string name)
        {
            var users = await _usersRerositry.GetUsersByName(userId, name);
            foreach (var user in users)
            {
                var session = await _sessionService.GetUserSessions(user.UserId);
                var isOnline = session.Count() > 0 ? true : false;
                user.UpdateStatus(isOnline);
            }
            return users;
        }

        public async Task<string> Registration(string email, string password, string name)
        {
            var mes = UserModel.DataValidation(email, password, name);
            if (!string.IsNullOrEmpty(mes)) return mes;

            var emailIsBusy = await _usersRerositry.GetUserBusy(email);
            if (emailIsBusy) return "This email is already busy";

            var passwordHash = _passwordHasher.Generate(password);

            var userId = Guid.NewGuid();

            var result = await _usersRerositry.Create(userId, email, passwordHash, name);
            if (!result) return "Error";

            return string.Empty;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRerositry.GetUserByEmail(email);
            if (string.IsNullOrEmpty(user.Password) || !_passwordHasher.Verify(password, user.Password))
                return string.Empty;

            var token = _jwtProvider.GenerateToken(user.UserId.ToString());

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
