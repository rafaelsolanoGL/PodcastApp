using System;
namespace PodcastApp.Domain
{
    public class Timestamp
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Section { get; set; }
        public Podcast Podcast { get; set; }
        public int PodcastId { get; set; }
    }
}
