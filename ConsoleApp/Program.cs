using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PodcastApp.Data;
using PodcastApp.Domain;

namespace ConsoleApp
{
    internal class Program
    {
        private static PodcastContext _context;

        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PodcastContext>();
            

            _context = new PodcastContext(optionsBuilder.UseSqlite("Data Source =  /Users/rafa/Projects/PodcastDB.db").Options);
            
            _context.Database.EnsureCreated();
            /*GetPodcasts("Before Add:");
            InsertMultiplePodcasts();            
            GetPodcasts("After Add:");
            InsertBattle();
            QueryAndUpdateBattle_Disconnected();
                InsertNewPodcastWithAQuote();
            AddQuoteToExistingPodcastWhileTracked();*/
       //         GetRawSQL();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

       /* private static void AddPodcast()
        {
            var samurai = new Podcast { Name = "Julie" };
            _context.Podcasts.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertMultiplePodcasts()
        {
            var samurai = new Podcast { Name = "Sampson4" };
            var samurai2 = new Podcast { Name = "Tasha4" };
            var samurai3 = new Podcast { Name = "Sampson5" };
            var samurai4 = new Podcast { Name = "Tasha5" };
            var samurai5 = new Podcast { Name = "Tasha6" };
            _context.Podcasts.AddRange(samurai, samurai2, samurai3, samurai4, samurai5);
            _context.SaveChanges();
        }

        private static void InsertVariousTypes()
        {
            var samurai = new Podcast { Name = "Kikuchio" };
            var clan = new Clan { ClanName = "Imperial Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }

        private static void GetPodcasts(string text)
        {
            var samurais = _context.Podcasts.ToList();
            Console.WriteLine($"{text}: Podcast count is {samurais.Count}");
            foreach(var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void QueryFilters()
        {
            var samurais = _context.Podcasts.Where(s => s.Name == "Sampson").ToList();
        }

        private static void RetrieveAndUpdatePodcast()
        {
            var samurai = _context.Podcasts.First();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultiplePodcasts()
        {
            var samurais = _context.Podcasts.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new PodcastContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertNewPodcastWithAQuote()
        {
            var samurai = new Podcast
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "I've come to save you" }
                }
            };
            _context.Podcasts.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddQuoteToExistingPodcastWhileTracked()
        {
            var samurai = _context.Podcasts.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }

        private static void ProjectPodcastsWithQuotes()
        {
            var somePropertiesWithQuotes = _context.Podcasts
                .Select(s => new { s.Id, s.Name, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) }).ToList();
        }

        private static void GetPodcastWithBattles()
        {
            var samuraiWithBattles = _context.Podcasts
                .Include(s => s.PodcastBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 2);

            var samuraiWithBattlesCleaner = _context.Podcasts.Where(s => s.Id == 2)
                .Select(s => new { Podcast = s, Battles = s.PodcastBattles.Select(sb => sb.Battle) });

        }

        private static void AddNewPodcastWithHorse()
        {
            var samurai = new Podcast { Name = "Jina Ujichika" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Podcasts.Add(samurai);
            _context.SaveChanges();

        }

        private static void GetRawSQL ()
        {
            var samurais = _context.Podcasts.FromSqlRaw("SELECT * from Podcasts").ToList();

        }*/
    }
}
