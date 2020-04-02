using System;
using System.ComponentModel.DataAnnotations;

namespace PodcastAPI.Models
{
    public class HostDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TwitterHandle { get; set; }
    }
}
