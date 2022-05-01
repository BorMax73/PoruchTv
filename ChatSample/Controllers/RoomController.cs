 using System;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Mvc;
 using poruchTv.Data;
 using poruchTv.Models;

 namespace poruchTv.Controllers
{
    public class RoomController : Controller
    {
        private UserContext db;

        public RoomController(UserContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string key, string url)
        {
            
            if (User?.Identity.IsAuthenticated == true)
            {
                var filmId = db.contents.FirstOrDefault(x => x.iframe_src == url);
                await db.Histories.AddAsync(new History() {Time = DateTime.Now, UserId = User.Identity.Name, FilmId = filmId!.id});
                await db.SaveChangesAsync();
            }
            return View();
        }
    }
}