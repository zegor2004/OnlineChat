using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Abstractions.Hub;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Abstractions.User.Session;
using Chat.Domain.Models.Chat.Message;
using Chat.Domain.Models.User;
using Chat.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services.Hub
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ISessionService _sessionService;

        public ChatHubService(IHubContext<ChatHub> hubContext, ISessionService sessionService)
        {
            _hubContext = hubContext;
            _sessionService = sessionService;
        }

        public async Task NotificationNewMessage(MessageModel message, Guid userId)
        {
            var connectionList = await _sessionService.GetUserSessions(userId);
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
            var connectionList = await _sessionService.GetUserSessions(message.UserId);
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
