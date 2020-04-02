using System;
using System.Collections.Generic;

namespace PodcastAPI.Models
{
    public class PodcastHostCommandModel
    {

        public List<int> Hosts { get; set; }
        
    }

    public class PodcastViewModel
    {
        public int PodcastId { get; set; }
        public List<String> Hosts { get; set; }
    }
}
