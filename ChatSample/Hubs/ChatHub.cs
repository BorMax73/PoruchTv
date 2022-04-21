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
       
        public async Task Enter(string roomId, string username)
        {
            this.ChannelName = roomId;
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                await Clients.Group(roomId).SendAsync("Notify", $"{username} вошел в чат");
            }
        }

        public async Task Leave(string roomId, string username)
        {
            if (!String.IsNullOrEmpty(roomId) && !String.IsNullOrEmpty(username))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
                await Clients.Group(roomId).SendAsync("Notify", $"{username}");
                
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
        public async Task Play(string roomId, double seek)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.Group(roomId).SendAsync("Play", roomId, seek);
        }

        public async Task Pause(string roomId, double seek)
        {
            await Clients.Group(roomId).SendAsync("Pause", roomId, seek);
        }
        public async Task SetTime(string roomId,double time)
        {
            await Clients.Group(roomId).SendAsync("SetTime", time);
        }
    }
}