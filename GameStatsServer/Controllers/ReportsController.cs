﻿using GameStatsServer.DataProviders;
using GameStatsServer.Entities;
using GameStatsServer.Extensions;
using GameStatsServer.Models;
using GameStatsServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStatsServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Reports")]
    public class ReportsController : Controller
    {
        private readonly IDbContext dbContext;
        private readonly INormalizeReportsCount normalizeReportsCount;
        public ReportsController(IDbContext dbContext, INormalizeReportsCount normalizeReportsCount)
        {
            this.dbContext = dbContext;
            this.normalizeReportsCount = normalizeReportsCount;
        }

        //GET api/reports/recent-matches/{count?}
        [HttpGet("recent-matches/{count?}")]
        public async Task<MatchInfoWithEndpointAndTimestamp[]> RecentMatches(int? count)
        {
            var _count = normalizeReportsCount.Normalize(count);

            return await dbContext.Matches
                .OrderByDescending(m => m.Timestamp)
                .Take(_count)
                .Select(m => m.CreateMatchInfoWithEndpointAndTimestamp())
                .ToArrayAsync();
        }

        //GET api/reports/best-players/{count?}
        [HttpGet("best-players/{count?}")]
        public async Task<BestPlayerInfo[]> BestPlayers(int? count)
        {
            var _count = normalizeReportsCount.Normalize(count);

            return await dbContext.Scores
                .GroupBy(score => score.PlayerName)
                .Where(group => group.Count() >= 10)
                .Where(group => group.Sum(score => score.Deaths) != 0)
                .OrderByDescending(group => GetPlayerKillToDeathRatio(group))
                .Take(_count)
                .Select(group => new BestPlayerInfo
                {
                    Name = group.Key,
                    KillToDeathRatio = GetPlayerKillToDeathRatio(group)
                })
                .ToArrayAsync();
        }

        private double GetPlayerKillToDeathRatio(IGrouping<string, Score> group)
            => (double)group.Sum(g => g.Kills) / group.Sum(g => g.Deaths);

        //GET api/reports/popular-servers/{count?}
        [HttpGet("popular-servers/{count?}")]
        public async Task<PopularServerInfo[]> PopularServers(int? count)
        {
            if (!await dbContext.Matches.AnyAsync())
                return new PopularServerInfo[0];
            var _count = normalizeReportsCount.Normalize(count);

            var lastTimestamp = await dbContext.Matches.MaxAsync(m => m.Timestamp);
            return await dbContext.Servers
                .OrderByDescending(s => GetServerAverageMatchesPerDay(s, lastTimestamp))
                .Take(_count)
                .Select(s => s.CreatePopularServerInfo(lastTimestamp))
                .ToArrayAsync();
        }

        private double GetServerAverageMatchesPerDay(Server server, DateTime lastTimestamp)
        {
            var serverAge = server.Matches.Any()
                ? lastTimestamp.AddDays(1).Date - server.Matches.Min(m => m.Timestamp).Date
                : new TimeSpan(1, 0, 0, 0);
            return (double)server.Matches.Count / serverAge.Days;
        }
    }
}