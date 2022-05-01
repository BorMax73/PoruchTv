using System;

namespace poruchTv.Models
{
    public class History
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FilmId { get; set; }
        public DateTime Time { get; set; }
    }
}