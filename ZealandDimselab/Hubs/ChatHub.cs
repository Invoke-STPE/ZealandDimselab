using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(MessageModel message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
