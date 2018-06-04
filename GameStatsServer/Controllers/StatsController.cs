using GameStatsServer.DataProviders;
using GameStatsServer.Extensions;
using GameStatsServer.Models;
using GameStatsServer.Services.StatusCodeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStatsServer.Controllers
{
    [ResponseCache(Duration = 60)]
    [Produces("application/json")]
    [Route("api")]
    public class StatsController : Controller
    {
        private readonly IDbContext dbContext;
        private readonly INotFoundService notFound;
        public StatsController(IDbContext dbContext, INotFoundService notFound)
        {
            this.dbContext = dbContext;
            this.notFound = notFound;
        }
        
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
            if (server == null)
                notFound.Set(ControllerContext.HttpContext);
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
            if (playerScores.Any())
                return playerScores.CreateStats(lastTimestamp);
            else
            {
                notFound.Set(ControllerContext.HttpContext);
                return null;
            }
        }
    }
}