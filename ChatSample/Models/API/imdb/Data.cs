using System.Collections.Generic;

namespace poruchTv.Models.API.imdb
{
    public class Data
    {
        //public int Id { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string overview { get; set; }
        public double vote_average { get; set; }
        public double popularity { get; set; }
        public List<int> genre_ids { get; set; }
    }
}