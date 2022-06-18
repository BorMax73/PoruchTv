using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using poruchTv.Data;
using poruchTv.Models;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    enum Genre
    {
        
    }
    public class FilmsController : Controller
    {
        private UserContext db;
        public FilmsController(UserContext context)
        {
            db = context;
        }

        //public async Task<IActionResult> Index(int pageNumber = 1)
        //{
        //    ViewData["yearStart"] = 1910;
        //    ViewData["yearEnd"] = 2022;
        //    ViewData["rattingLow"] = 0;
        //    ViewData["rattingHigh"] = 10;
        //    var result = db.contents.OrderByDescending(x => x.popularity).Where(x => x.content_type == "movie");

        //    return View(await PaginatedList<Content>.CreateAsync(result, pageNumber, 15));
        //}
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            ViewData["yearStart"] = 1910;
            ViewData["yearEnd"] = 2022;
            ViewData["rattingLow"] = 0;
            ViewData["rattingHigh"] = 10;
            var result = db.ContentInfos.OrderByDescending(x => x.Popularity).Where(x => x.ContentType == "movie");

            return View(await PaginatedList<ContentInfo>.CreateAsync(result, pageNumber, 15));
        }

        [HttpGet]
        public async Task<ViewResult> GetFilms(int yearStart, int yearEnd, double rattingLow, double rattingHigh, Genres input,
            int pageNumber = 1)
        {
            ViewData["yearStart"] = 1910;
            ViewData["yearEnd"] = 2022;
            ViewData["rattingLow"] = 0;
            ViewData["rattingHigh"] = 10;

            if (yearStart != 0) ViewData["yearStart"] = yearStart;

            if (yearEnd != 0) ViewData["yearEnd"] = yearEnd;

            if (rattingLow >= 0) ViewData["rattingLow"] = rattingLow;

            if (rattingHigh != 0) ViewData["rattingHigh"] = rattingHigh;

            var b = input.GenresInput.Where(x => x.Value == true).Select(x => x.Key).ToList();

            var result = db.ContentInfos.Where(x =>
                (int) (object) x.ReleaseDate.Substring(0, 4) >= yearStart &&
                (int) (object) x.ReleaseDate.Substring(0, 4) <= yearEnd && x.VoteAverage >= rattingLow &&
                x.VoteAverage <= rattingHigh);

            // result.Join(b, x=>x.genre_ids.Split(','), p=>p, {});
            foreach (var ganre in b)
            {
               result = result.Where(x => x.Genre.Contains(ganre.ToString()));
            }

            //result.Where(x=>Array.ConvertAll(x.genre_ids.Split(','), b=>int.Parse(b)) )
            return View("Index", await PaginatedList<ContentInfo>.CreateAsync(result.OrderByDescending(x => x.Popularity), pageNumber, 15));
        }

        private bool IsInRange(int check, int start, int end)
        {
            if (check >= start && check <= end)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}