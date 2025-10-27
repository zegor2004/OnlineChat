using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.User.Session;

namespace Chat.Application.Services.User.Session
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<List<string>> GetSessionUserByUserId(Guid userId)
        {
            return await _sessionRepository.Get(userId);
        }
    }
}
