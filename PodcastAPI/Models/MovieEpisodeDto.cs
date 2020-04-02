using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PodcastAPI.Models
{
    public class MovieEpisodeDto
    {
        
        public int MovieId { get; set; }

        public int EpisodeId { get; set; }

        [Required]
        public int HostId { get; set; }

        public String Opinion { get; set; }

        [Required]
        public bool IsMainReview { get; set; }

    }
}
