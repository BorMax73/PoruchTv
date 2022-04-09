using System;

namespace poruchTv.Models.Video
{
    public class Content
    {
        public int id { get; set; }
        public string title { get; set; }

        public string? kp_id { get; set; }

        public string year { get; set; }
        public string orig_title { get; set; }
        [field: NonSerialized]
        public string imgUrl { get; set; }

        public string iframe_src { get; set; }
    }
}