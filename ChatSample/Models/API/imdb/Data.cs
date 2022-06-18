using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace poruchTv.Models.API.imdb
{
    
    public class Data
    {
        //public int Id { get; set; }
        public string poster_path { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public double vote_average { get; set; }
        public double popularity { get; set; }
        public List<int> genre_ids { get; set; }
    }
}