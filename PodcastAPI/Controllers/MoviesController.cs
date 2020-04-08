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
    public class MoviesController : Controller
    {

        public MoviesController(PodcastContext context)
        {
            MovieLogic.SetContext(context);
            PodcastLogic.SetContext(context);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie([FromBody]MovieForCreationDto movie)
        {
            var returnMovie = await MovieLogic.CreateMovieAsync(movie);
            return CreatedAtAction("GetMovie", new { id = returnMovie.Id }, returnMovie);
        }

        // POST api/values
        [HttpPost("{id}/episode/{episodeId}")]
        public async Task<ActionResult<MovieDto>> AddMovieToEpisode(int id, int episodeId, [FromBody]MovieEpisodeDto movieEpisode)
        {
            movieEpisode.MovieId = id;
            movieEpisode.EpisodeId = episodeId;
            if (!MovieLogic.MovieExists(id))
                return NotFound();
            if (!PodcastLogic.PodcastExists(episodeId))
                return NotFound();
            var returnMovie = await MovieLogic.AddMovieToEpisodeAsync(movieEpisode);
            return CreatedAtAction("GetMovie", new { id = returnMovie.Id }, returnMovie);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var Movies = await MovieLogic.GetMoviesAsync();
            return Ok(Movies);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var Movie = await MovieLogic.GetMovieAsync(id);
            if (Movie == null)
                return NotFound();
            return Ok(Movie);
        }


        
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> UpdateMovie(int id, [FromBody]MovieDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            if (!MovieLogic.MovieExists(id))
            {
                return NotFound();
            }
            MovieDto returnMovie = await MovieLogic.UpdateMovieAsync(movie);
            return Ok(returnMovie);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDto>> DeleteAsync(int id)
        {
            if (!MovieLogic.MovieExists(id))
            {
                return NotFound();
            }
            var Movie = await MovieLogic.DeleteMovieAsync(id);
            return Ok(Movie);
        }

    }
}
