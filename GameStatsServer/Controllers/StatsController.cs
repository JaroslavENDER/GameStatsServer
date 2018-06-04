using GameStatsServer.DataProviders;
using GameStatsServer.Extensions;
using GameStatsServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStatsServer.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class StatsController : Controller
    {
        private readonly IDbContext dbContext;
        public StatsController(IDbContext dbContext)
            => this.dbContext = dbContext;


        //GET api/servers/{endpoint}/stats
        [HttpGet("servers/{endpoint}/stats")]
        public async Task<ServerStats> ServerStats(string endpoint)
        {
            var server = await dbContext.Servers
                .Include(s => s.Matches).ThenInclude(m => m.Scores)
                .FirstOrDefaultAsync(s => s.Endpoint == endpoint);
            var lastTimestamp = await dbContext.Matches.AnyAsync()
                ? await dbContext.Matches.MaxAsync(m => m.Timestamp)
                : DateTime.Now;
            return server?.CreateServerStats(lastTimestamp);
        }

        //GET api/players/{name}/stats
        [HttpGet("players/{name}/stats")]
        public async Task<PlayerStats> PlayerStats(string name)
        {
            var playerScores = await dbContext.Scores
                .Include(s => s.Match).ThenInclude(m => m.Server)
                .Where(s => s.PlayerName.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            var lastTimestamp = await dbContext.Matches.AnyAsync()
                ? await dbContext.Matches.MaxAsync(m => m.Timestamp)
                : DateTime.Now;
            return playerScores.Any()
                ? playerScores.CreateStats(lastTimestamp)
                : null;
        }
    }
}