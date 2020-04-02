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
    public class HostLogic
    {
        private static PodcastContext _context;

        public HostLogic(PodcastContext context)
        {
            _context = context;
        }

        public static void SetContext(PodcastContext context)
        {
            _context = context;
        }

        public static async System.Threading.Tasks.Task<HostDto> CreateHostAsync(HostForCreationDto host)
        {
            Host newHost = new Host { Name = host.Name, TwitterHandle = host.TwitterHandle };
            _context.Hosts.Add(newHost);
            await _context.SaveChangesAsync();
            return new HostDto { Id = newHost.Id, Name = newHost.Name, TwitterHandle = newHost.TwitterHandle };
        }

        public static async System.Threading.Tasks.Task<ICollection<HostDto>> GetHostsAsync()
        {
            return await _context.Hosts.Select(x => new HostDto { Id = x.Id, Name = x.Name, TwitterHandle = x.TwitterHandle }).ToListAsync();
        }

        public static async System.Threading.Tasks.Task<HostDto> GetHostAsync(int id)
        {
            return await _context.Hosts.Select(x => new HostDto { Id = x.Id, Name = x.Name, TwitterHandle = x.TwitterHandle }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<HostDto> UpdateHostAsync(HostDto host)
        {

            Host hostToUpdate = new Host { Id = host.Id, Name = host.Name, TwitterHandle = host.TwitterHandle, PodcastHosts = new List<PodcastHost>() };
            _context.Entry(hostToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return await _context.Hosts.Select(x => new HostDto { Id = x.Id, Name = x.Name, TwitterHandle = x.TwitterHandle }).FirstOrDefaultAsync(x => x.Id == host.Id);
        }

        public static bool HostExists(int id)
        {
            return _context.Hosts.Any(e => e.Id == id);
        }

        public static async Task<HostDto> DeleteHostAsync(int id)
        {
            var host = await _context.Hosts.FindAsync(id);
            _context.Hosts.Remove(host);
            await _context.SaveChangesAsync();
            return new HostDto { Id = host.Id, Name = host.Name, TwitterHandle = host.TwitterHandle };
        }
    }
}
