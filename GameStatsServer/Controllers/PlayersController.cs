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
    [Route("api/Players")]
    public class PlayersController : Controller
    {
        private IDbContext dbContext;
        public PlayersController(IDbContext dbContext)
            => this.dbContext = dbContext;

        //GET api/players/{name}/stats
        [HttpGet("{name}/stats")]
        public async Task<PlayerStats> Stats(string name)
        {
            var playerScores = await dbContext.Scores
                .Where(s => s.PlayerName.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            var lastTimestamp = await dbContext.Matches.MaxAsync(m => m.Timestamp);
            return playerScores.CreateStats(lastTimestamp);
        }
    }
}