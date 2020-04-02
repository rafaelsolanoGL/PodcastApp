using System;
using System.ComponentModel.DataAnnotations;

namespace PodcastAPI.Models
{
    public class MovieForUpdateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Director { get; set; }
        
    }
}
