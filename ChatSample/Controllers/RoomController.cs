﻿ using System;
 using System.Linq;
 using System.Threading.Tasks;
 using ChatSample.Hubs;
 using Customs.Services;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.SignalR;
 using Microsoft.EntityFrameworkCore;
 using poruchTv.Data;
 using poruchTv.Models;

 namespace poruchTv.Controllers
{
    public class RoomController : Controller
    {
        private UserContext db;
        private IOnlineService service;
        private IHubContext<NotificationHub> hub;
        public RoomController(UserContext context, IOnlineService srv, IHubContext<NotificationHub> notificationHub)
        {
            db = context;
            service = srv;
            hub= notificationHub;
        }
        public async Task<IActionResult> Index(string key, string url)
        {
            
            if (User?.Identity.IsAuthenticated == true)
            {
                var filmId = db.contents.FirstOrDefault(x => x.iframe_src == url);
                await db.Histories.AddAsync(new History() {Time = DateTime.Now, UserId = User.Identity.Name, FilmId = filmId!.id});
                await db.SaveChangesAsync();
            }
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = HttpContext.User.Identity.Name;
                var friends = db.Friendships.Where(x => x.IsConfirm == true &
                                                        x.FirstUserName == userName ||
                                                        x.SecondUserName == userName);
                var query1 = friends.Select(x => x.FirstUserName);
                var query2 = friends.Select(x => x.SecondUserName);
                var query3 = query1.Where(x => x != userName);
                query3 = query3.Concat(query2.Where(x => x != userName));
                var onlineUsers = service.GetOnlineUsers();
                
                return View(onlineUsers.Intersect(query3));
            }
            return View();
        }

        [HttpPost]
        public async Task Invite(string name, string url)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
                
            }

            var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == name);
            if (user == null)
                return;
            await hub.Clients.User(user.Id).SendAsync("invite", "test", url);
        }

    }
}