using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace ChatSample.Filters
{
    public class OnlineStatus
    {
        private readonly RequestDelegate _next;

        public OnlineStatus(RequestDelegate next)
        {
            this._next = next;
        }

        private Dictionary<string, DateTime> OnlineUsers = new Dictionary<string, DateTime>();
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated == true)
            {
                if (context.User.Identity.Name != null && OnlineUsers.ContainsKey(context.User.Identity.Name))
                {
                    OnlineUsers[context.User.Identity.Name] = DateTime.Now;
                }
                else
                {
                    OnlineUsers.Add(context.User.Identity.Name, DateTime.Now);
                }
            }

            await Task.Run(DeleteOfflineUsers);
            await _next.Invoke(context);
        }

        private void DeleteOfflineUsers()
        {
            foreach (var user in OnlineUsers)
            {
                if (user.Value < DateTime.Now.AddMinutes(-5))
                {
                    OnlineUsers.Remove(user.Key);
                }
            }
        }
    }
}