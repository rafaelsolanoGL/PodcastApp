using System;
using System.ComponentModel.DataAnnotations;

namespace PodcastAPI.Models
{
    public class HostForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TwitterHandle { get; set; }
    }
}
