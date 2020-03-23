using System;
using System.Collections.Generic;

namespace PodcastApp.Domain
{
    public class Host
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwitterHandle { get; set; }
        public List<PodcastHost> PodcastHosts { get; set; }
    }
}
