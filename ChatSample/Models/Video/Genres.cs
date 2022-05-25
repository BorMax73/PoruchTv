using System.Collections.Generic;

namespace poruchTv.Models.Video
{
    public class Genres
    {
        public Dictionary<int, bool> GenresInput { get; set; }

        public Genres()
        {
            GenresInput = new Dictionary<int, bool>()
            {
                {28, false},
                {12, false},
                {16, false},
                {35, false},
                {80, false},
                {99, false},
                {18, false},
                {10751, false},
                {14, false},
                {36, false},
                {27, false},
                {10402, false},
                {9648, false},
                {10749, false},
                {878, false},
                {10770, false},
                {53, false},
                {10752, false},
                {37, false},

            };
        }
    }
}