using Microsoft.EntityFrameworkCore;
using PodcastApp.Domain;

namespace PodcastApp.Data
{
    public class PodcastContext: DbContext
    {

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public PodcastContext() : base(new DbContextOptionsBuilder().UseSqlite("Data Source =  /Users/rafa/Projects/PodcastDB.db").Options)
        {

        }
        
        public PodcastContext(DbContextOptions<PodcastContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PodcastHost>().HasKey(s => new { s.PodcastId, s.HostId });
            modelBuilder.Entity<PodcastMovie>().HasKey(s => new { s.PodcastId, s.HostId, s.MovieId });
            modelBuilder.Entity<PodcastHost>()
            .HasOne(bc => bc.Podcast)
            .WithMany(b => b.PodcastHosts)
            .HasForeignKey(bc => bc.PodcastId);
            modelBuilder.Entity<PodcastHost>()
                .HasOne(bc => bc.Host)
                .WithMany(c => c.PodcastHosts)
                .HasForeignKey(bc => bc.HostId);
        }
    }
}
