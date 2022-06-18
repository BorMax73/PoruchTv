using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace poruchTv.Models.Video
{
    public class ContentUrls
    {
        [field: NonSerialized]
        public int Id { get; set; }
        [field: NonSerialized]
        public int ContentInfoId { get; set; }
        public string Title { get; set; }
        
        [JsonProperty("iframe_src")]
        public string Url { get; set; }
    }
}