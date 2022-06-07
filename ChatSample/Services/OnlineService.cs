using System.Collections.Generic;
using EllipticCurve.Utils;

namespace Customs.Services
{
    public class OnlineService : IOnlineService
    {
        private List<string> OnlineUsers = new List<string>();
        public List<string> GetOnlineUsers()
        {
            return OnlineUsers;
            
        }

        public void AddUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            OnlineUsers.Add(name);
        }

        public void RemoveUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            OnlineUsers.Remove(name);
        }
    }
}