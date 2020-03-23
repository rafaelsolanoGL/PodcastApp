using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApp.Data;
using PodcastApp.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostAPI.Controllers
{
    [Route("api/[controller]")]
    public class HostsController : Controller
    {

        private readonly PodcastContext _context;

        public HostsController(PodcastContext context)
        {
            _context = context;
        }


        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Host>>> GetHosts()
        {
            return await _context.Hosts.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Host>> GetHost(int id)
        {
            var Host = await _context.Hosts.FindAsync(id);
            if (Host == null)
                return NotFound();
            return Host;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Host>> PostHost([FromBody]Host Host)
        {
            _context.Hosts.Add(Host);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHost", new { id = Host.Id }, Host);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHost(int id, [FromBody]Host Host)
        {
            if (id != Host.Id)
            {
                return BadRequest();
            }

            _context.Entry(Host).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HostExists(id))
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
        public async Task<ActionResult<Host>> DeleteAsync(int id)
        {
            var Host = await _context.Hosts.FindAsync(id);
            if (Host == null)
            {
                return NotFound();
            }
            _context.Hosts.Remove(Host);
            await _context.SaveChangesAsync();
            return Host;
        }

        

        private bool HostExists (int id)
        {
            return _context.Hosts.Any(e => e.Id == id);
        }
    }
}
