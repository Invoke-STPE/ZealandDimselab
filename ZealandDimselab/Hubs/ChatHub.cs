using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        private static Dictionary<string, List<string>> supports = new Dictionary<string, List<string>>();
        private static Dictionary<string, List<string>> students = new Dictionary<string, List<string>>();
        private static Dictionary<string, string> activeChats = new Dictionary<string, string>();
        public override Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;
            SaveConnection(name, connectionId);
            if (Context.User.IsInRole("admin") == false)
            {
                AssignUserToSupport(name);
            }
            return base.OnConnectedAsync();
        }


        public Task SendMessage(MessageModel message)
        {
            string sender = Context.User.Identity.Name;
            var receiverId = "";
            string receiver = "";
            List<string> connections = new List<string>();
            if (activeChats.ContainsKey(sender)) // Kunde som afsender
            {
                receiver = activeChats[sender];
                connections = supports[receiver];
            }
            if (activeChats.Values.Any(x => x.ToLower() == sender.ToLower()))
            {
                //receiver = activeChats.Values.FirstOrDefault(x => x.ToLower() == sender.ToLower());
                receiver = activeChats.FirstOrDefault(x => x.Value.ToLower() == sender.ToLower()).Key;
                connections = students[receiver];
            }
            return Clients.Clients(connections).SendAsync("ReceiveMessage", message);
            //return Clients.All.SendAsync("ReceiveMessage", message);
        }
        private void SaveConnection(string name, string connectionId)
        {
            if (Context.User.IsInRole("admin"))
            {
                if (supports.ContainsKey(name))
                {
                    supports[name].Add(connectionId);
                }
                else
                {
                    supports.Add(name, new List<string> { connectionId });
                }
            }
            else
            {
                if (students.ContainsKey(name))
                {
                    students[name].Add(connectionId);
                }
                else
                {
                    students.Add(name, new List<string> { connectionId });
                }
            }
        }
        private void AssignUserToSupport(string student)
        {
            if (activeChats.ContainsKey(student) == false)
            {
                Random random = new Random();
                string support = supports.ElementAt(random.Next(0, supports.Count)).Key;
     
                activeChats.Add(student, support);
            }
           
        }


    }
}
