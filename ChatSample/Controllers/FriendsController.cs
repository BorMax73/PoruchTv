using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poruchTv.Data;
using poruchTv.Models;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    public class FriendsController : Controller
    {
        private UserContext db;
        public FriendsController(UserContext context)
        {
            db = context;
        }
        public async Task<ActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                var userName = HttpContext.User.Identity.Name;
                var friends = db.Friendships.Where(x => x.IsConfirm == true &
                                                              x.FirstUserName == userName ||
                                                              x.SecondUserName == userName);
                var query1 = friends.Select(x => x.FirstUserName);
                var query2 = friends.Select(x => x.SecondUserName);
                var query3 = query1.Where(x=>x != userName);
                query3 = query3.Concat(query2.Where(x => x != userName));
                var friendRequests = await db.Friendships.Where(x => x.IsConfirm == false & x.SecondUserName == userName).ToListAsync();
                FriendsPage result = new FriendsPage()
                    {FriendNames = await query3.ToListAsync(), Friendships = friendRequests};
                return View(result);
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
            
        }
        [HttpGet]
        public IActionResult SearchUsers(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Redirect("/Friends");
            }
            var users = db.Users.Where(x => EF.Functions.Like(x.UserName, $"%{name}%")).AsEnumerable();
            
            return PartialView("SearchUsers", users);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriend(int id)
        {
            if (id < 0)
            {
                return Redirect("/Home");
            }

            var friend = await db.Friendships.FirstOrDefaultAsync(x => x.Id == id & x.IsConfirm == false);
            if (friend == null)
            {
                return Redirect("/Home");
            }
            friend.IsConfirm = true;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            if (id < 0)
            {
                return Redirect("/Home");
            }

            var friend = await db.Friendships.FirstOrDefaultAsync(x => x.Id == id & x.IsConfirm == false);
            if (friend == null)
            {
                return Redirect("/Home");
            }

            db.Friendships.Remove(friend);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
