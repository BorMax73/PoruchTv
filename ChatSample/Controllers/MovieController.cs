using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChatSample.db;
using Microsoft.AspNetCore.Mvc;
using poruchTv.Models;
using poruchTv.Models.API;
using poruchTv.Models.Random;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationContext db;
        public MovieController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var result = db.contents.FirstOrDefault(x => x.id == id);
            
            return View(result);
        }

        [HttpPost]
        public IActionResult Create(string urlI)
        {
            var key = Randomaiser.RandomString(6);
            return RedirectToAction("Index", "Room", new{key= $"{key}", url = urlI });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}