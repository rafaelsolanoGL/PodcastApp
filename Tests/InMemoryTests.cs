using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PodcastApp.Data;
using PodcastApp.Domain;

namespace Tests
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertPodcastIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PodcastContext>();
            builder.UseInMemoryDatabase("PodcastTestingDatabase");
            using (var context = new PodcastContext(builder.Options))
            {
                var podcast = new Podcast();
                podcast.Title = "Test";
                context.Podcasts.Add(podcast);
                context.SaveChanges();
                Debug.WriteLine($"Inserted ID: {podcast.Id}");

                Assert.AreNotEqual(0, podcast.Id);
            }
        }

        [TestMethod]
        public void CanReadPodcastFromDatabase()
        {
            CanInsertPodcastIntoDatabase();
            var builder = new DbContextOptionsBuilder<PodcastContext>();
            builder.UseInMemoryDatabase("PodcastTestingDatabase");
            using (var context = new PodcastContext(builder.Options))
            {
                var podcast = context.Podcasts.FirstOrDefault(x => x.Title == "Test");
                var podcasts = context.Podcasts.ToList();
                Debug.WriteLine($"Selected ID: {podcast.Id}");

                Assert.AreNotEqual(0, podcast.Id);
            }
        }

        [TestMethod]
        public void CanInsertHostIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PodcastContext>();
            builder.UseInMemoryDatabase("PodcastTestingDatabase");
            using (var context = new PodcastContext(builder.Options))
            {
                var host = new Host();
                context.Hosts.Add(host);
                context.SaveChanges();
                Debug.WriteLine($"Inserted ID: {host.Id}");

                Assert.AreNotEqual(0, host.Id);
            }
        }

        [TestMethod]
        public void CanReadHostFromDatabase()
        {
            var builder = new DbContextOptionsBuilder<PodcastContext>();
            builder.UseInMemoryDatabase("CanInsertHost");
            using (var context = new PodcastContext(builder.Options))
            {
                var host = context.Hosts.FirstOrDefault();
                Debug.WriteLine($"Selected ID: {host.Id}");

                Assert.AreNotEqual(0, host.Id);
            }
        }

        [TestMethod]
        public void CanInsertMovieIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PodcastContext>();
            builder.UseInMemoryDatabase("CanInsertMovie");
            using (var context = new PodcastContext(builder.Options))
            {
                var movie = new Movie();
                context.Movies.Add(movie);
                context.SaveChanges();
                Debug.WriteLine($"Inserted ID: {movie.Id}");

                Assert.AreNotEqual(0, movie.Id);
            }
        }

        [TestMethod]
        public void CanReadMoviesFromDatabase()
        {
            var builder = new DbContextOptionsBuilder<PodcastContext>();
            builder.UseInMemoryDatabase("CanInsertMovie");
            using (var context = new PodcastContext(builder.Options))
            {
                var movie = context.Movies.FirstOrDefault();
                Debug.WriteLine($"Selected ID: {movie.Id}");

                Assert.AreNotEqual(0, movie.Id);
            }
        }



    }

}
