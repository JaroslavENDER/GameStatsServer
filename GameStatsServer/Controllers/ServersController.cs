using GameStatsServer.DataProviders;
using GameStatsServer.Extensions;
using GameStatsServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStatsServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Servers")]
    public class ServersController : Controller
    {
        private IDbContext dbContext;
        public ServersController(IDbContext dbContext)
            => this.dbContext = dbContext;

        //GET api/servers/info
        [HttpGet("info")]
        public async Task<IEnumerable<ServerInfoWithEndpoint>> Info()
        {
            return await dbContext.Servers.Select(server => server.CreateServerInfoWithEndpoint()).ToListAsync();
        }

        //GET api/servers/{endpoint}/info
        [HttpGet("{endpoint}/info")]
        public async Task<ServerInfo> Info(string endpoint)
        {
            var server = await dbContext.Servers.FindAsync(endpoint);
            return server?.CreateServerInfo() ?? null;
        }

        //PUT api/servers/{endpoint}/info
        [HttpPut("{endpoint}/info")]
        public async Task Info(string endpoint, [FromBody]ServerInfo value)
        {
            dbContext.Servers.Add(value.CreateServer(endpoint));
            await dbContext.SaveChangesAsync();
        }

        //GET api/servers/{endpoint}/stats
        [HttpGet("{endpoint}/stats")]
        public async Task<ServerStats> Stats(string endpoint)
        {
            var server = await dbContext.Servers.FindAsync(endpoint);
            var lastTimestamp = await dbContext.Matches.MaxAsync(m => m.Timestamp);
            return server?.CreateServerStats(lastTimestamp);
        }

        //GET api/servers/{endpoint}/matches/{timestamp}
        [HttpGet("{endpoint}/matches/{timestamp}")]
        public async Task<MatchInfo> Match(string endpoint, string timestamp)
        {
            var match = await dbContext.Matches.FindAsync(endpoint, timestamp);
            return match?.CreateMatchInfo() ?? null;
        }

        //PUT api/servers/{endpoint}/matches/{timestamp}
        [HttpPut("{endpoint}/matches/{timestamp}")]
        public async Task Match(string endpoint, string timestamp, [FromBody]MatchInfo value)
        {
            var server = await dbContext.Servers.FindAsync(endpoint);
            server?.Mathes.Add(value.CreateMatch(timestamp));
            await dbContext.SaveChangesAsync();
        }
    }
}
