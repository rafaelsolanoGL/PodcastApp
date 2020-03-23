using System;
namespace PodcastApp.Domain
{
    public class PodcastHost
    {
        public int PodcastId { get; set; }
        public int HostId { get; set; }
        public Podcast Podcast { get; set; }
        public Host Host { get; set; }
    }
}
