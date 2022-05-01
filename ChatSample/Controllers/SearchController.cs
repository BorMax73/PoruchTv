using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using poruchTv.Data;
using poruchTv.Models;
using poruchTv.Models.API;
using poruchTv.Models.API.imdb;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    public class SearchController : Controller
    {
        private UserContext db;
        public SearchController(UserContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string name, int pageNumber = 1)
        {
            ViewData["name"] = name;
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest();

            }

            var queryable = db.contents.Where(x => EF.Functions.Like(x.orig_title, $"%{name}%") || EF.Functions.Like(x.ru_title, $"%{name}%"));
            //var ruQueryable = db.contents.Where(x => EF.Functions.Like(x.ru_title, $"%{name}%"));
            var result = queryable.OrderByDescending(x => x.popularity);
            return View(await PaginatedList<Content>.CreateAsync(result, pageNumber, 15));
        }
        //public async Task<IActionResult> Index(string name, int page = 1)
        //{

        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        List<Content> result = new List<Content>();

        //        for (int i = 1; i < 5; i++)
        //        {
        //            HttpResponseMessage response = await client.GetAsync(
        //                $"https://videocdn.tv/api/tv-series?api_token=WwashE4xCvKmEQjBktjGAIngTQL1R0Bp&page={i}&limit=100");
        //            response.EnsureSuccessStatusCode();
        //            string responseBody = await response.Content.ReadAsStringAsync();
        //            var k = JsonConvert.DeserializeObject<ResultApi>(responseBody);

        //            foreach (Content b in k.data)
        //            {
        //                if (b.imdb_id != null & !b.orig_title.Contains("#"))
        //                {


        //                    var res = await client.GetAsync(
        //                        $"https://api.themoviedb.org/3/find/{b.imdb_id}?api_key=15d2ea6d0dc1d476efbca3eba2b9bbfb&query=" +
        //                        "&language=uk" + "&external_source=imdb_id");
        //                    if (res != null)
        //                    {
        //                        var json = await res.Content.ReadAsStringAsync();
        //                        var obj = JsonConvert.DeserializeObject<SearchData>(json);
        //                        if (obj != null && obj.results.Count > 0)
        //                        {
        //                            var sort = obj.results.FirstOrDefault(x =>
        //                                !String.IsNullOrEmpty(b.year) && !String.IsNullOrEmpty(x.release_date) &&
        //                                x.release_date.Substring(0, 4) == b.year.Substring(0, 4));
        //                            if (sort != null)
        //                            {

        //                                b.imgUrl = "https://image.tmdb.org/t/p/w500" + sort.poster_path;
        //                                b.overview = sort.overview;
        //                                b.popularity = sort.popularity;
        //                                b.genre_ids = String.Join(',', sort.genre_ids);
        //                                b.vote_average = sort.vote_average;

        //                            }
        //                        }


        //                    } 
        //                    b.id = 0;
        //                    result.Add(b);
        //                    Debug.Write($"WORK{i}");
        //                }


        //            }

        //        }


        //        db.contents.AddRange(result);
        //        db.SaveChanges();
        //        Debug.Write("FINAL");

        //        return null;

        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Debug.WriteLine("\nException Caught!");
        //        Debug.WriteLine("Message :{0} ", e.Message);
        //        return null;

        //    }


        //}


    }

}
