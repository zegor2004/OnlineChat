using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Abstractions.Hub;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Abstractions.User.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Chat.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatHubService _chatHubService;
        private readonly ISessionService _sessionService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatHub(ISessionService sessionService, IChatHubService chatHubService, IChatService chatService, IUserService userService )
        {
            _chatHubService = chatHubService;
            _sessionService = sessionService;
            _chatService = chatService;
            _userService = userService;
        }
        public override async Task OnConnectedAsync()
        {
            var nameIdentifier = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null)
            {
                Context.Abort();
                return;
            }
            var userId = new Guid(nameIdentifier.Value);
            await _sessionService.CreateSession(Context.ConnectionId, userId);
            var user = await _userService.GetUserByUserId(userId);
            user.UpdateStatus(true);
            var usersChats = await _chatService.GetChatPreview(user.UserId);
            foreach (var userChat in usersChats)
            {
                var connectionList = await _sessionService.GetUserSessions(userChat.User.UserId);
                if (connectionList.Count > 0)
                {
                    foreach (var connection in connectionList)
                    {
                        await Clients.Client(connection).SendAsync("UpdateStatusUser", user);
                    }
                }
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var nameIdentifier = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null)
            {
                Context.Abort();
                return;
            }
            var userId = new Guid(nameIdentifier.Value);
            await _sessionService.DeleteSession(Context.ConnectionId);
            var user = await _userService.GetUserByUserId(userId);
            user.UpdateStatus(false);
            var usersChats = await _chatService.GetChatPreview(user.UserId);
            foreach (var userChat in usersChats)
            {
                var connectionList = await _sessionService.GetUserSessions(userChat.User.UserId);
                if (connectionList.Count > 0)
                {
                    foreach (var connection in connectionList)
                    {
                        await Clients.Client(connection).SendAsync("UpdateStatusUser", user);
                    }
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
