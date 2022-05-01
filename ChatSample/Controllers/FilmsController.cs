using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using poruchTv.Data;
using poruchTv.Models;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    public class FilmsController : Controller
    {
        private UserContext db;
        public FilmsController(UserContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var result = db.contents.OrderByDescending(x => x.popularity).Where(x => x.content_type == "movie");
            
            return View(await PaginatedList<Content>.CreateAsync(result, pageNumber, 15));
        }
    }
}