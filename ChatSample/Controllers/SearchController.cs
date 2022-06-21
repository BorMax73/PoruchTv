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
        private readonly UserContext db;
        public SearchController(UserContext context)
        {
            db = context;
        }

        //public async Task<IActionResult> Index(string name, int pageNumber = 1)
        //{
        //    ViewData["name"] = name;
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        return BadRequest();

        //    }

        //    var queryable = db.ContentInfos.Where(x => EF.Functions.Like(x.OriginalTitle, $"%{name}%") || EF.Functions.Like(x.Title, $"%{name}%"));
        //    //var ruQueryable = db.contents.Where(x => EF.Functions.Like(x.ru_title, $"%{name}%"));
        //    var result = queryable.OrderByDescending(x => x.Popularity);
        //    return View(await PaginatedList<ContentInfo>.CreateAsync(result, pageNumber, 15));
        //}
        //public async Task<IActionResult> Index(string name, int page = 1)
        //{

        //    try
        //    {
        //        HttpClient client = new HttpClient();


        //        for (int i = 279; i < 300; i++)
        //        {
        //            //List<ContentInfo> contentInfos = new List<ContentInfo>();
        //            //List<ContentUrls> contentUrls = new List<ContentUrls>();
        //            HttpResponseMessage response = await client.GetAsync(
        //                $"https://videocdn.tv/api/movies?api_token=WwashE4xCvKmEQjBktjGAIngTQL1R0Bp&page={i}&limit=100");
        //            response.EnsureSuccessStatusCode();

        //            string responseBody = await response.Content.ReadAsStringAsync();
        //            var k = JsonConvert.DeserializeObject<ResultApi>(responseBody);

        //            if (k != null)
        //                foreach (Content b in k.data)
        //                {
        //                    if (b.imdb_id != null & !b.orig_title.Contains("#"))
        //                    {
        //                        var res = await client.GetAsync(
        //                            $"https://api.themoviedb.org/3/find/{b.imdb_id}?api_key=15d2ea6d0dc1d476efbca3eba2b9bbfb&query=" +
        //                            "&language=uk" + "&external_source=imdb_id");
        //                        if (res != null)
        //                        {
        //                            var json = await res.Content.ReadAsStringAsync();
        //                            var obj = JsonConvert.DeserializeObject<SearchData>(json);
        //                            if (obj != null)
        //                            {
        //                                if (obj!.movie_results.Count > 0)
        //                                {


        //                                    //var sort = obj.movie_results.FirstOrDefault(x =>
        //                                    //    !String.IsNullOrEmpty(b.year) && !String.IsNullOrEmpty(x.release_date) &&
        //                                    //    x.release_date.Substring(0, 4) == b.year.Substring(0, 4));
        //                                    //if (sort != null)
        //                                    //{
        //                                    var sort = obj.movie_results[0];
        //                                    ContentInfo info = new ContentInfo()
        //                                    {
        //                                        ContentType = b.content_type,
        //                                        Genre = String.Join(',', sort.genre_ids),
        //                                        Id = 0,
        //                                        ImageUrl = "https://image.tmdb.org/t/p/w500" + sort.poster_path,
        //                                        OriginalTitle = sort.original_title,
        //                                        Overview = sort.overview,
        //                                        Popularity = sort.popularity,
        //                                        ReleaseDate = sort.release_date,
        //                                        Title = sort.title,
        //                                        VoteAverage = sort.vote_average
        //                                    };
        //                                    await db.ContentInfos.AddAsync(info);
        //                                    await db.SaveChangesAsync();
        //                                    foreach (var t in b.translations)
        //                                    {
        //                                        t.ContentInfoId = info.Id;
        //                                        t.Id = 0;
        //                                        await db.ContentUrls.AddAsync(t);
        //                                    }

        //                                    await db.SaveChangesAsync();

        //                                    //b.imgUrl = "https://image.tmdb.org/t/p/w500" + sort.poster_path;
        //                                    //b.overview = obj.movie_results[0].overview;
        //                                    //b.popularity = sort.popularity;
        //                                    //b.genre_ids = String.Join(',', sort.genre_ids);
        //                                    //b.vote_average = sort.vote_average;

        //                                    //}
        //                                }
        //                            }
        //                        }

        //                        b.id = 0;

        //                        Debug.Write($"WORK{i}");
        //                    }
        //                }
        //        }


        //        //db.contents.AddRange(result);
        //        //db.SaveChanges();
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
