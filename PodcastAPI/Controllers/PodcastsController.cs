using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PodcastApp.BusinessLogic;
using PodcastApp.Data;
using PodcastApp.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PodcastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PodcastsController : Controller
    {
        public PodcastsController(PodcastContext context)
        {
            PodcastLogic.SetContext(context);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<PodcastDto>> CreatePodcast([FromBody]PodcastForCreationDto Podcast)
        {
            var returnPodcast = await PodcastLogic.CreatePodcastAsync(Podcast);
            return CreatedAtAction("GetPodcast", new { id = returnPodcast.Id }, returnPodcast);
        }

        // POST api/values
        [HttpPost("{id}/timestamp")]
        public async Task<ActionResult<MovieDto>> AddTimestampToEpisode(int id, [FromBody]TimestampForCreationDto timestamp)
        {
            if (!PodcastLogic.PodcastExists(id))
                return NotFound();

            timestamp.PodcastId = id;
            var returnPodcast = await PodcastLogic.AddTimestampToPodcastAsync(timestamp);
            return CreatedAtAction("GetPodcast", new { id = returnPodcast.Id }, returnPodcast);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PodcastDto>>> GetPodcasts()
        {
            var Podcasts = await PodcastLogic.GetPodcastsAsync();
            return Ok(Podcasts);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PodcastWithMoviesDto>> GetPodcast(int id)
        {
            var Podcast = await PodcastLogic.GetPodcastAsync(id);
            if (Podcast == null)
                return NotFound();
            return Ok(Podcast);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PodcastDto>> UpdatePodcast(int id, [FromBody]PodcastDto podcast)
        {
            if (id != podcast.Id)
            {
                return BadRequest();
            }
            if (!PodcastLogic.PodcastExists(id))
            {
                return NotFound();
            }
            PodcastDto returnPodcast = await PodcastLogic.UpdatePodcastAsync(podcast);
            return Ok(returnPodcast);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PodcastDto>> DeletePodcast(int id)
        {
            if (!PodcastLogic.PodcastExists(id))
            {
                return NotFound();
            }
            var podcast = await PodcastLogic.DeletePodcastAsync(id);
            return Ok(podcast);
        }

    }
}
