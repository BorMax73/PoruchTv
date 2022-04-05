using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        internal string ChannelName;

        //public ChatHub(string channelName)
        //{
        //    _channelName = channelName;
        //}
       
        public async Task Enter(string groupname, string username)
        {
            this.ChannelName = groupname;
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                await Clients.Group(ChannelName).SendAsync("Notify", $"{username} вошел в чат");
            }
        }
        //public async Task AddToGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        //}

        //public async Task RemoveFromGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        //}
        public async Task Play(string command, double seek)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.Group(ChannelName).SendAsync("Play", command, seek);
        }

        public async Task Pause(string command, double seek)
        {
            await Clients.Group(ChannelName).SendAsync("Pause", command, seek);
        }
        public async Task SetTime(double time)
        {
            await Clients.All.SendAsync("SetTime", time);
        }
    }
}