using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApp.Data;
using PodcastApp.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PodcastAPI.Controllers
{
    [Route("api/[controller]")]
    public class PodcastsController : Controller
    {

        private readonly PodcastContext _context;

        public PodcastsController(PodcastContext context)
        {
            _context = context;
        }


        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Podcast>>> GetPodcasts()
        {
            return await _context.Podcasts.Include(x => x.PodcastHosts).ToListAsync();
        }

        [HttpGet("{id}/2gen/")]
        public async Task<ActionResult<IEnumerable<Podcast>>> GetPodcasts2Gen()
        {
            return await _context.Podcasts.Include(x => x.PodcastHosts).ThenInclude(ph => ph.Host).ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Podcast>> GetPodcast(int id)
        {
            var podcast = await _context.Podcasts.FindAsync(id);
            if (podcast == null)
                return NotFound();
            return podcast;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Podcast>> PostPodcast([FromBody]Podcast podcast)
        {
            _context.Podcasts.Add(podcast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPodcast", new { id = podcast.Id }, podcast);
        }

        // POST api/values/
        [HttpPost("{id}/hosts/")]
        public async Task<ActionResult<Podcast>> PostPodcastHosts(int id, List<int> hosts)
        {
            List<PodcastHost> podcastHosts= new List<PodcastHost>();
            foreach(int host in hosts)
            {
                _context.Add(new PodcastHost { PodcastId=id, HostId=host });
            }
            await _context.SaveChangesAsync();
            var podcastWithHosts = _context.Podcasts.Where(p => p.Id == id)
                .Select(x => new
                {
                    Podcast = x,
                    Hosts = x.PodcastHosts.Select(ph => ph.Host)
                });
            return CreatedAtAction("GetPodcast", new { podcastWithHosts });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPodcast(int id, [FromBody]Podcast podcast)
        {
            if (id != podcast.Id)
            {
                return BadRequest();
            }

            _context.Entry(podcast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodcastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Podcast>> DeleteAsync(int id)
        {
            var podcast = await _context.Podcasts.FindAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }
            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();
            return podcast;
        }

        

        private bool PodcastExists (int id)
        {
            return _context.Podcasts.Any(e => e.Id == id);
        }
    }
}
