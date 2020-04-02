using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Models;
using PodcastApp.BusinessLogic;
using PodcastApp.Data;

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
        /*
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PodcastDto>> UpdatePodcast(int id, [FromBody]PodcastDto Podcast)
        {
            if (id != Podcast.Id)
            {
                return BadRequest();
            }
            if (!PodcastLogic.PodcastExists(id))
            {
                return NotFound();
            }
            PodcastDto returnPodcast = await PodcastLogic.UpdatePodcastAsync(Podcast);
            return Ok(returnPodcast);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PodcastDto>> DeleteAsync(int id)
        {
            if (!PodcastLogic.PodcastExists(id))
            {
                return NotFound();
            }
            var Podcast = await PodcastLogic.DeletePodcastAsync(id);
            return Ok(Podcast);
        }*/

    }
}
