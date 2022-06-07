using System.Linq;
using System.Threading.Tasks;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poruchTv.Areas.Identity.Data;
using poruchTv.Data;
using poruchTv.Models;

namespace poruchTv.Controllers
{
    public class UserController : Controller
    {
        private UserContext db;
        public UserController(UserContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return NotFound();
            }
            var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == name);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Redirect("/Home");
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userName = HttpContext.User.Identity.Name;
            if (db.Friendships.Any(x=>(x.FirstUserName == userName | x.FirstUserName == name) & (x.SecondUserName == userName | x.SecondUserName == name) ))
            {
                return Redirect("/Home");
            }

            await db.Friendships.AddAsync(new Friendship()
                {FirstUserName = userName, SecondUserName = name, IsConfirm = false});
            await db.SaveChangesAsync();
            return await Index(name);
        }
    }
}
