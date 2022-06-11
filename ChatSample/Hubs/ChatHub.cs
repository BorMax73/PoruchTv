#nullable enable
using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using poruchTv.Areas.Identity.Data;
using poruchTv.Data;
using poruchTv.Models.Rooms;

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
      
        public async Task Enter(string roomId, string username, double seek, string link)
        {
            
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
            }
            else
            {
                var room = await db.Rooms.FirstOrDefaultAsync(x => x.Name == roomId);
                if (room == null)
                {
                    await db.Rooms.AddAsync(new Room() { Created = DateTime.Now, Name = roomId, FilmUrl = link });
                    await db.SaveChangesAsync();
                }
                var connectionId = Context.ConnectionId;
                await db.UserSessions.AddAsync(new UserSession() { Name = username, RoomId = roomId, SessionId = connectionId, Url = link });
                await db.SaveChangesAsync();
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                Users.TryAdd(username, seek);
                var users = await db.UserSessions.Where(x => x.RoomId == roomId).Select(x => x.Name).ToListAsync();
                await Clients.Group(roomId).SendAsync("Notify", users);
            }
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await db.UserSessions.FirstOrDefaultAsync(x => x.SessionId == Context.ConnectionId);
            if (user != null) db.UserSessions.Remove(user);
            await db.SaveChangesAsync();
            if (user != null)
            {
                await Groups.RemoveFromGroupAsync(user.RoomId, "SignalR Users");
                var users = await db.UserSessions.Where(x => x.RoomId == user.RoomId).Select(x => x.Name).ToListAsync();
                await Clients.Group(user.RoomId).SendAsync("Notify", users);
            }

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