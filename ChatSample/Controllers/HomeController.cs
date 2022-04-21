using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ChatSample.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using poruchTv.Models;
using poruchTv.Models.API;
using poruchTv.Models.Random;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        //[HttpGet]
        //public async Task<IActionResult> Search(string name, int page =1)
        //{
        //    var content = await SearchAPI.Search(name,page);

        //    return View("Index",content);
        //}
        [HttpGet]
        public IActionResult GetFilm(Content movie)
        {
            
            return RedirectToAction("Index", "Movie", movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
