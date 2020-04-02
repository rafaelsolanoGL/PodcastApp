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
    public class PodcastLogic
    {
        private static PodcastContext _context;

        public PodcastLogic(PodcastContext context)
        {
            _context = context;
        }

        public static void SetContext(PodcastContext context)
        {
            _context = context;
        }

        public static async System.Threading.Tasks.Task<PodcastWithMoviesDto> CreatePodcastAsync(PodcastForCreationDto podcast)
        {
            Podcast newPodcast = new Podcast { Length = podcast.Length, Number = podcast.Number, RecordingDate = podcast.RecordingDate, ReleaseDate = podcast.ReleaseDate, Timestamps = new List<Timestamp>(), Title = podcast.Title, PodcastHosts = podcast.Hosts.Select(x => new PodcastHost { HostId = x.Id }).ToList() };
            _context.Podcasts.Add(newPodcast);
            await _context.SaveChangesAsync();

            return await GetPodcastAsync(newPodcast.Id);
        }

        public static async System.Threading.Tasks.Task<ICollection<PodcastDto>> GetPodcastsAsync()
        {
            return await _context.Podcasts.Include(x => x.PodcastHosts).ThenInclude(x => x.Host)
                .Select(x => new PodcastDto
                {
                    Id = x.Id,
                    Length = x.Length,
                    Number = x.Number,
                    RecordingDate = x.RecordingDate,
                    ReleaseDate = x.ReleaseDate,
                    Title = x.Title,
                    Hosts = x.PodcastHosts
                    .Select(x => new HostDto
                    {
                        Id = x.Host.Id,
                        Name = x.Host.Name,
                        TwitterHandle = x.Host.TwitterHandle
                    }).ToList()
                }).ToListAsync();
        }

        public static async System.Threading.Tasks.Task<PodcastWithMoviesDto> GetPodcastAsync(int id)
        {
            return await _context.Podcasts.Include(x => x.PodcastHosts).ThenInclude(x => x.Host)
                .Include(m => m.PodcastMovies).ThenInclude(m => m.Movie)
                .Select(x => new PodcastWithMoviesDto
                {
                    Id = x.Id,
                    Length = x.Length,
                    Number = x.Number,
                    RecordingDate = x.RecordingDate,
                    ReleaseDate = x.ReleaseDate,
                    Title = x.Title,
                    Hosts = x.PodcastHosts
                    .Select(x => new HostDto
                    {
                        Id = x.Host.Id,
                        Name = x.Host.Name,
                        TwitterHandle = x.Host.TwitterHandle
                    }).ToList(),
                     Movies = x.PodcastMovies.Select (x => new ReviewDto {  HostName = x.Host.Name, IsMainReview = x.IsMainReview, Opinion = x.Opinion, Title = x.Movie.Title, Number = x.Podcast.Number, EpisodeId = x.Podcast.Id, ReleaseDate = x.Podcast.ReleaseDate }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        /*
        public static async Task<PodcastDto> UpdatePodcastAsync(PodcastDto Podcast)
        {

            Podcast PodcastToUpdate = new Podcast { Id = Podcast.Id, Name = Podcast.Name, TwitterHandle = Podcast.TwitterHandle, PodcastPodcasts = new List<PodcastPodcast>() };
            _context.Entry(PodcastToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return await _context.Podcasts.Select(x => new PodcastDto { Id = x.Id, Name = x.Name, TwitterHandle = x.TwitterHandle }).FirstOrDefaultAsync(x => x.Id == Podcast.Id);
        }



        public static async Task<PodcastDto> DeletePodcastAsync(int id)
        {
            var Podcast = await _context.Podcasts.FindAsync(id);
            _context.Podcasts.Remove(Podcast);
            await _context.SaveChangesAsync();
            return new PodcastDto { Id = Podcast.Id, Name = Podcast.Name, TwitterHandle = Podcast.TwitterHandle };
        }
        */

        public static bool PodcastExists(int id)
        {
            return _context.Podcasts.Any(e => e.Id == id);
        }

    }
}
