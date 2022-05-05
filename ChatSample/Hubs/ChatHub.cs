﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using poruchTv.Areas.Identity.Data;
using poruchTv.Data;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        private UserContext db;

        public ChatHub(UserContext context)
        {
            db=context;
        }
        internal static ConcurrentDictionary<string, double> Users = new ConcurrentDictionary<string, double>();
      
        public async Task Enter(string roomId, string username, double seek)
        {
           var connectionId=  Context.ConnectionId;
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                Users.TryAdd(username, seek);
                await Clients.Group(roomId).SendAsync("Notify", Users);
            }
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
       
        //public async Task Leave(string roomId, string username)
        //{
        //    if (!String.IsNullOrEmpty(roomId) && !String.IsNullOrEmpty(username))
        //    {
        //        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        //        double seek = 0;
        //        Users.TryRemove(username, out seek);
        //        await Clients.OthersInGroup(roomId).SendAsync("Notify", Users);
                
        //    }
        //}
      
        public async Task Play(string roomId, double seek)
        {
            await Clients.OthersInGroup(roomId).SendAsync("Play", roomId, seek);
        }

        public async Task Pause(string roomId, double seek)
        {
            await Clients.OthersInGroup(roomId).SendAsync("Pause", roomId, seek);
        }
        public async Task SetTime(string roomId,double time)
        {
            await Clients.OthersInGroup(roomId).SendAsync("SetTime", time);
        }
        public async Task Send(string message, string roomId, string username)
        {
            await Clients.Group(roomId).SendAsync("Send", message, username);
            
        }
    }
}