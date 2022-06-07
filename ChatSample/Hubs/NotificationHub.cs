using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Customs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using poruchTv.Areas.Identity.Data;
using poruchTv.Data;

namespace ChatSample.Hubs
{
    public class NotificationHub : Hub
    {
        private UserContext db;
        private IOnlineService service;
        public NotificationHub(UserContext context, IOnlineService onlineService)
        {
            db = context;
            service = onlineService;
        }
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                service.AddUser(Context.User.Identity.Name);
                var count = db.Friendships.Count(x => x.SecondUserName == Context.User.Identity.Name & x.IsConfirm == false);
                await Clients.Caller.SendAsync("FriendCount", count);
            }
            

        }
    }
} 