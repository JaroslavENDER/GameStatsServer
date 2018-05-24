using GameStatsServer.DataProviders;
using GameStatsServer.Extensions;
using GameStatsServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IEnumerable<ServerInfoWithEndpoint>> AllInfo()
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
        public async void Info(string endpoint, [FromBody]ServerInfo value)
        {
            await dbContext.Servers.AddAsync(value.CreateServer(endpoint));
            await dbContext.SaveChangesAsync();
        }

        //GET api/servers/{endpoint}/stats
        [HttpGet("{endpoint}/stats")]
        public string Stats(string endpoint)
        {
            //TODO: Models.ServerStats
            throw new NotImplementedException("//TODO: Models.ServerStats");
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
        public async void Match(string endpoint, string timestamp, [FromBody]MatchInfo value)
        {
            var server = await dbContext.Servers.FindAsync(endpoint);
            server?.Mathes.Add(value.CreateMatch(timestamp));
            await dbContext.SaveChangesAsync();
        }
    }
}
