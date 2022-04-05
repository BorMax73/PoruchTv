using Microsoft.AspNetCore.Mvc;

namespace poruchTv.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}