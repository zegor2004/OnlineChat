using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Hub;
using Chat.Domain.Abstractions.User.Session;
using Chat.Domain.Models.Chat.Message;
using Chat.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services.Hub
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ISessionRepository _sessionRepository;

        public ChatHubService(IHubContext<ChatHub> hubContext, ISessionRepository sessionRepository)
        {
            _hubContext = hubContext;
            _sessionRepository = sessionRepository;
        }

        public async Task NotificationNewMessage(MessageModel message, Guid userId)
        {
            var connectionList = await _sessionRepository.Get(userId);
            if (connectionList.Count > 0)
            {
                foreach (var connection in connectionList)
                {
                    await _hubContext.Clients.Client(connection).SendAsync("NewMessage", message);
                }
            }
        }

        public async Task UpdateStatusMessage(MessageModel message)
        {
            var connectionList = await _sessionRepository.Get(message.UserId);
            if (connectionList.Count > 0)
            {
                foreach (var connection in connectionList)
                {
                    await _hubContext.Clients.Client(connection).SendAsync("UpdateStatusMessage", message);
                }
            }
        }
    }
}
