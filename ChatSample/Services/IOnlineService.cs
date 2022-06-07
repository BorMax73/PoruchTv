using System.Collections.Generic;

namespace Customs.Services
{
    public interface IOnlineService
    {
        List<string> GetOnlineUsers();

        void AddUser(string name);
        void RemoveUser(string name);
    }
}