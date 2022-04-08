using System.Diagnostics;
using System.Threading.Tasks;
using ChatSample.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poruchTv.Models;
using poruchTv.Models.API;
using poruchTv.Models.Random;

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
            var content = await SearchAPI.Search();
            return View(content);
        }
        [HttpPost]
        public IActionResult Create()
        {
            var key = Randomaiser.RandomString(6);
            return RedirectToAction("Index", "Room", $"{key}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
