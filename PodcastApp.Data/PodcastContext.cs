using Microsoft.EntityFrameworkCore;
using PodcastApp.Domain;

namespace PodcastApp.Data
{
    public class PodcastContext: DbContext
    {

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public PodcastContext(DbContextOptions<PodcastContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PodcastHost>().HasKey(s => new { s.PodcastId, s.HostId });
            modelBuilder.Entity<PodcastMovie>().HasKey(s => new { s.PodcastId, s.HostId, s.MovieId});
        }
    }
}
