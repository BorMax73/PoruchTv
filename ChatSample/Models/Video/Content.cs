using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poruchTv.Models.Video
{
    public class Content
    {
        [Key]
        [field: NonSerialized]
        public int id { get; set; }
        public string ru_title { get; set; }

        public string? kinopoisk_id { get; set; }
        public string? imdb_id { get; set; }
        public string year { get; set; }
        public string orig_title { get; set; }

        [field: NonSerialized]
        public string imgUrl { get; set; }
        
        public string iframe_src { get; set; }
        public string content_type { get; set; }
        [field: NonSerialized]
        public string overview { get; set; }
        [field: NonSerialized]
        public double popularity { get; set; }
        [field: NonSerialized]
        public double vote_average { get; set; }
        [field: NonSerialized]
        public string genre_ids { get; set; }

    }
}