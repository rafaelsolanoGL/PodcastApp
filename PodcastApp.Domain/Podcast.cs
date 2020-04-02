using System;
using System.Collections.Generic;

namespace PodcastApp.Domain
{
    public class Podcast
    {

        public Podcast()
        {
            Timestamps = new List<Timestamp>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime RecordingDate { get; set; }
        public string Length { get; set; }
        public List<Timestamp> Timestamps { get; set; }
        public List<PodcastHost> PodcastHosts { get; set; }
        public List<PodcastMovie> PodcastMovies { get; set; }
    }
}
