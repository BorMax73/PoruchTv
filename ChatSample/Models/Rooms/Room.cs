using System;

namespace poruchTv.Models.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string FilmUrl { get; set; }
    }
}