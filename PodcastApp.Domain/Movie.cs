using System;
using System.Collections.Generic;

namespace PodcastApp.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public List<PodcastMovie> PodcastMovies { get; set; }
    }
}
