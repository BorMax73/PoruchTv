using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poruchTv.Data;
using poruchTv.Models;
using poruchTv.Models.API;
using poruchTv.Models.Random;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    public class MovieController : Controller
    {
        private UserContext db;
        public MovieController(UserContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var result =await db.contents.FirstOrDefaultAsync(x => x.id == id);
            
            return View(result);
        }

        [HttpPost]
        public IActionResult Create(string urlI)
        {
            var key = Randomaiser.RandomString(10);
            return RedirectToAction("Index", "Room", new{key= $"{key}", url = urlI });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}