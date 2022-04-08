using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Json.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using poruchTv.Models.API.imdb;
using poruchTv.Models.Video;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace poruchTv.Models.API
{
    public class SearchAPI
    {
        private const string URL = "https://XXXX/rest/api/2/component";
        static readonly HttpClient client = new HttpClient();
        private const string DATA = @"{
            ""name"": ""Component 2"",
            ""description"": ""This is a JIRA component"",
            ""leadUserName"": ""xx"",
            ""assigneeType"": ""PROJECT_LEAD"",
            ""isAssigneeTypeValid"": false,
            ""project"": ""TP""}";

        public static async Task<List<Content>> Search()
        {
            try
            {
                //for(int i = 1; i < 3580; i++)
                //{
                string url = "https://api.imgbb.com/1/upload?key=0b339a62a2f5d568378183f74cde6f07&image=";
                    HttpResponseMessage response = await client.GetAsync($"https://videocdn.tv/api/short?api_token=WwashE4xCvKmEQjBktjGAIngTQL1R0Bp&page=1&limit=30");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);
                    var k = JsonConvert.DeserializeObject<ResultApi>(responseBody);
                    int count = 0;
                    List<Content> result = new List<Content>();
                    foreach (Content b in k.data)
                    {
                        if (b.kp_id != null)
                        {

                            var res = await client.GetAsync(
                                "https://api.themoviedb.org/3/search/movie?api_key=15d2ea6d0dc1d476efbca3eba2b9bbfb&query=" + b.orig_title);
                            if (res != null)
                            {
                               var json =   await res.Content.ReadAsStringAsync();
                               var obj = JsonConvert.DeserializeObject<SearchData>(json);
                               if (obj.results.Count > 0)
                               {
                                   var sort = obj.results.FirstOrDefault(x => !String.IsNullOrEmpty(b.year) && !String.IsNullOrEmpty(x.release_date) && x.release_date.Substring(0, 4) == b.year.Substring(0,4));
                                   if (sort != null) b.imgUrl = "https://image.tmdb.org/t/p/w500" + sort.poster_path;
                               }
                               

                            } //Debug.WriteLine(b.id);

                            //var B = await webclient.DownloadDataTaskAsync(new Uri($"http://st.kinopoisk.ru/images/film_big/{b.kp_id}.jpg"));
                            //                              //HttpResponseMessage response2 = await client.GetAsync(url + $"http://st.kinopoisk.ru/images/film_big/{b.kp_id}.jpg");
                            //                              //response2.EnsureSuccessStatusCode();
                            //                              //string responseBody2 = await response2.Content.ReadAsStringAsync();
                            //                              //var data = JsonConvert.DeserializeObject<SearchData>(responseBody2);
                            //b.imgUrl = B;
                            result.Add(b);
                            count++;
                        }


                    }


                    return result;


            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}