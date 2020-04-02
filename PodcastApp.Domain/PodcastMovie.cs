using System;
namespace PodcastApp.Domain
{
    public class PodcastMovie
    {
        public int PodcastId { get; set; }
        public int MovieId { get; set; }
        public int HostId { get; set; }
        public Podcast Podcast { get; set; }
        public Movie Movie { get; set; }
        public Host Host { get; set; }
        public String Opinion { get; set; }
        public bool IsMainReview { get; set; }
    }
}
