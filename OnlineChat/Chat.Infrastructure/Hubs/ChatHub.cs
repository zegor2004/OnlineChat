using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Abstractions.User.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Chat.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ISessionRepository _sessionRepository;
        public ChatHub(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
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
            await _sessionRepository.Add(Context.ConnectionId, userId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _sessionRepository.Delete(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        //public async Task NotificationNewMessage(string message, Guid userId)
        //{
        //    var connectionList = await _sessionRepository.Get(userId);
        //    if (connectionList.Count > 0)
        //    {
        //        foreach (var connection in connectionList)
        //        {
        //            await Clients.Client(connection).SendAsync("NewMessage", message);
        //        }
        //    }
            
        //}
    }
}
