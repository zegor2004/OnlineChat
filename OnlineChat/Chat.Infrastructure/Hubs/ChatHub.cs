using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {

        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("NewMessage", message);
        }
    }
}
