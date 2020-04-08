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
    public class HostsController : Controller
    {

        public HostsController(PodcastContext context)
        {
            HostLogic.SetContext(context);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<HostDto>> CreateHost([FromBody]HostForCreationDto host)
        {
            var returnHost = await HostLogic.CreateHostAsync(host);
            return CreatedAtAction("GetHost", new { id = returnHost.Id }, returnHost);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HostDto>>> GetHosts()
        {
            var hosts = await HostLogic.GetHostsAsync();
            return Ok(hosts);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HostDto>> GetHost(int id)
        {
            var host = await HostLogic.GetHostAsync(id);
            if (host == null)
                return NotFound();
            return Ok(host);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<HostDto>> UpdateHost(int id, [FromBody]HostDto host)
        {
            if (id != host.Id)
            {
                return BadRequest();
            }
            if (!HostLogic.HostExists(id))
            {
                return NotFound();
            }
            HostDto returnHost = await HostLogic.UpdateHostAsync(host);
            return Ok(returnHost);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HostDto>> DeleteHost(int id)
        {
            if (!HostLogic.HostExists(id))
            {
                return NotFound();
            }
            var host = await HostLogic.DeleteHostAsync(id);
            return Ok(host);
        }

    }
}
