namespace poruchTv.Models.Video
{
    public class ContentInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string ImageUrl { get; set; }
        public string ContentType { get; set; }

        public string Overview { get; set; }
        public string Genre { get; set; }
        public double Popularity { get; set; }
        
        public double VoteAverage { get; set; }

        public string ReleaseDate { get; set; }
    }
}