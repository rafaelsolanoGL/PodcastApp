using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PodcastAPI.Models
{
    public class PodcastForUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime RecordingDate { get; set; }

        [Required]
        public string Length { get; set; }

        //public List<Timestamp> Timestamps { get; set; }
        public List<HostDto> Hosts { get; set; }
    }
}
