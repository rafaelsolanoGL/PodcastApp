using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PodcastAPI.Models;
using PodcastApp.Data;
using PodcastApp.Domain;

namespace PodcastApp.BusinessLogic
{
    public class MovieLogic
    {
        private static PodcastContext _context;

        public MovieLogic(PodcastContext context)
        {
            _context = context;
        }

        public static void SetContext(PodcastContext context)
        {
            _context = context;
        }

        public static async System.Threading.Tasks.Task<MovieDto> CreateMovieAsync(MovieForCreationDto movie)
        {
            Movie newMovie = new Movie { Director = movie.Director, Title = movie.Title };
            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();

            return await GetMovieAsync(newMovie.Id);
        }

        public static async System.Threading.Tasks.Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            return await _context.Movies.Include(x => x.PodcastMovies).ThenInclude(x => x.Podcast)
                .Include(h => h.PodcastMovies).ThenInclude(h => h.Host)
                .Select(x => new MovieDto
                {
                    Id = x.Id,
                    Director = x.Director,
                    Title = x.Title,
                    Reviews = x.PodcastMovies
                    .Select(x => new ReviewDto
                    {
                        EpisodeId = x.Podcast.Id,
                        Title = x.Podcast.Title,
                         Number = x.Podcast.Number,
                         ReleaseDate = x.Podcast.ReleaseDate,
                         HostName = x.Host.Name,
                         Opinion = x.Opinion,
                         IsMainReview = x.IsMainReview
                       
                    }).ToList()
                }).ToListAsync();
        }

        public static async System.Threading.Tasks.Task<MovieDto> AddMovieToEpisodeAsync (MovieEpisodeDto movieEpisode)
        {
            PodcastMovie podcastMovie = new PodcastMovie() { HostId = movieEpisode.HostId, MovieId = movieEpisode.MovieId, Opinion = movieEpisode.Opinion, PodcastId = movieEpisode.EpisodeId, IsMainReview = movieEpisode.IsMainReview };

            _context.Add(podcastMovie);
            await _context.SaveChangesAsync();
            return await GetMovieAsync(movieEpisode.MovieId);

        }

        public static async System.Threading.Tasks.Task<MovieDto> GetMovieAsync(int id)
        {
            return await _context.Movies.Include(x => x.PodcastMovies).ThenInclude(x => x.Podcast)
                .Include(h => h.PodcastMovies).ThenInclude(h => h.Host)
                .Select(x => new MovieDto
                {
                    Id = x.Id,
                    Director = x.Director,
                    Title = x.Title,
                    Reviews = x.PodcastMovies
                    .Select(x => new ReviewDto
                    {
                        EpisodeId = x.Podcast.Id,
                        Title = x.Podcast.Title,
                        Number = x.Podcast.Number,
                        ReleaseDate = x.Podcast.ReleaseDate,
                        HostName = x.Host.Name,
                        Opinion = x.Opinion,
                        IsMainReview = x.IsMainReview

                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        /*
        public static async Task<MovieDto> UpdateMovieAsync(MovieDto Movie)
        {

            Movie MovieToUpdate = new Movie { Id = Movie.Id, Name = Movie.Name, TwitterHandle = Movie.TwitterHandle, MovieMovies = new List<MovieMovie>() };
            _context.Entry(MovieToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return await _context.Movies.Select(x => new MovieDto { Id = x.Id, Name = x.Name, TwitterHandle = x.TwitterHandle }).FirstOrDefaultAsync(x => x.Id == Movie.Id);
        }



        public static async Task<MovieDto> DeleteMovieAsync(int id)
        {
            var Movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(Movie);
            await _context.SaveChangesAsync();
            return new MovieDto { Id = Movie.Id, Name = Movie.Name, TwitterHandle = Movie.TwitterHandle };
        }
        */

        public static bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

    }
}
