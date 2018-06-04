using GameStatsServer.DataProviders;
using GameStatsServer.Extensions;
using GameStatsServer.Models;
using GameStatsServer.Services.StatusCodeServices;
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
        private readonly IDbContext dbContext;
        private readonly IBadRequestService badRequest;
        private readonly INotFoundService notFound;
        public ServersController(IDbContext dbContext, IBadRequestService badRequest, INotFoundService notFound)
        {
            this.dbContext = dbContext;
            this.badRequest = badRequest;
            this.notFound = notFound;
        }

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
            if (server == null)
                notFound.Set(ControllerContext.HttpContext);
            return server?.CreateServerInfo() ?? null;
        }

        //PUT api/servers/{endpoint}/info
        [HttpPut("{endpoint}/info")]
        public async Task Info(string endpoint, [FromBody]ServerInfo value)
        {
            if (value == null || !value.IsValidModel())
            {
                badRequest.Set(ControllerContext.HttpContext);
                return;
            }

            var server = await dbContext.Servers.FindAsync(endpoint);
            if (server != null)
                value.Rewrite(server);
            else
                dbContext.Servers.Add(value.CreateServer(endpoint));
            await dbContext.SaveChangesAsync();
        }

        //GET api/servers/{endpoint}/matches/{timestamp}
        [HttpGet("{endpoint}/matches/{timestamp}")]
        public async Task<MatchInfo> Match(string endpoint, string timestamp)
        {
            var server = await dbContext.Servers
                .Include(s => s.Matches)
                .FirstOrDefaultAsync(s => s.Endpoint == endpoint);
            if (server == null)
            {
                notFound.Set(ControllerContext.HttpContext);
                return null;
            }
            var match = server.Matches.FirstOrDefault(m => m.Timestamp == DateTime.Parse(timestamp));
            if (match == null)
                notFound.Set(ControllerContext.HttpContext);
            return match?.CreateMatchInfo() ?? null;
        }

        //PUT api/servers/{endpoint}/matches/{timestamp}
        [HttpPut("{endpoint}/matches/{timestamp}")]
        public async Task Match(string endpoint, string timestamp, [FromBody]MatchInfo value)
        {
            if (value == null || !value.IsValidModel())
            {
                badRequest.Set(ControllerContext.HttpContext);
                return;
            }

            var server = await dbContext.Servers.FindAsync(endpoint);
            if (server == null)
                notFound.Set(ControllerContext.HttpContext);
            server?.Matches.Add(value.CreateMatch(timestamp));
            await dbContext.SaveChangesAsync();
        }
    }
}
