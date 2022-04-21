using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ChatSample.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using poruchTv.Models.API;
using poruchTv.Models.API.imdb;
using poruchTv.Models.Video;

namespace poruchTv.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationContext db;
        public SearchController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest();

            }

            var enQueryable = await db.contents.Where(x => EF.Functions.Like(x.orig_title, $"%{name}%")).OrderBy(x => x.popularity).ToListAsync();
            var ruQueryable = await db.contents.Where(x => EF.Functions.Like(x.ru_title, $"%{name}%")).OrderBy(x => x.popularity).ToListAsync();
            var result = new List<Content>() { };
            result.AddRange(enQueryable);
            result.AddRange(ruQueryable);
            return View(result);
        }
        //public async Task<IActionResult> Index(string name, int page = 1)
        //{

        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        List<Content> result = new List<Content>();

        //        for (int i = 200; i < 300; i++)
        //        {
        //            HttpResponseMessage response = await client.GetAsync(
        //                $"https://videocdn.tv/api/movies?api_token=WwashE4xCvKmEQjBktjGAIngTQL1R0Bp&page={i}&limit=100");
        //            response.EnsureSuccessStatusCode();
        //            string responseBody = await response.Content.ReadAsStringAsync();
        //            var k = JsonConvert.DeserializeObject<ResultApi>(responseBody);

        //            foreach (Content b in k.data)
        //            {
        //                if (b.imdb_id != null & !b.orig_title.Contains("#"))
        //                {


        //                    var res = await client.GetAsync(
        //                        "https://api.themoviedb.org/3/search/movie?api_key=15d2ea6d0dc1d476efbca3eba2b9bbfb&query=" +
        //                        b.orig_title);
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
        //                            }
        //                        }


        //                    } //Debug.WriteLine(b.id);

        //                    //var B = await webclient.DownloadDataTaskAsync(new Uri($"http://st.kinopoisk.ru/images/film_big/{b.kp_id}.jpg"));
        //                    //                              //HttpResponseMessage response2 = await client.GetAsync(url + $"http://st.kinopoisk.ru/images/film_big/{b.kp_id}.jpg");
        //                    //                              //response2.EnsureSuccessStatusCode();
        //                    //                              //string responseBody2 = await response2.Content.ReadAsStringAsync();
        //                    //                              //var data = JsonConvert.DeserializeObject<SearchData>(responseBody2);
        //                    //b.imgUrl = B;
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
