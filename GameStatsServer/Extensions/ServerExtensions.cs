using GameStatsServer.Entities;
using GameStatsServer.Models;
using System;
using System.Linq;

namespace GameStatsServer.Extensions
{
    public static class ServerExtensions
    {
        public static ServerInfo CreateServerInfo(this Server server)
        {
            return new ServerInfo
            {
                Name = server.Name,
                GameModes = server.GameModes.Split(", ")
            };
        }

        public static ServerInfoWithEndpoint CreateServerInfoWithEndpoint(this Server server)
        {
            return new ServerInfoWithEndpoint
            {
                Endpoint = server.Endpoint,
                Info = CreateServerInfo(server)
            };
        }

        public static ServerStats CreateServerStats(this Server server, DateTime lastTimestamp)
        {
            if (!server.Matches.Any())
                return new ServerStats
                {
                    TotalMatchesPlayed = 0,
                    MaximunMatchesPerDay = 0,
                    AverageMatchesPerDay = 0,
                    MaximunPopulation = 0,
                    AveragePopulation = 0,
                    Top5GameModes = new string[0],
                    Top5Maps = new string[0]
                };

            var serverAge = lastTimestamp.AddDays(1).Date - server.Matches.Min(m => m.Timestamp).Date;
            return new ServerStats
            {
                TotalMatchesPlayed = server.Matches.Count,
                MaximunMatchesPerDay = server.Matches.GroupBy(m => m.Timestamp.Date).Max(g => g.Count()),
                AverageMatchesPerDay = (double)server.Matches.Count / serverAge.Days,
                MaximunPopulation = server.Matches.Max(m => m.Scores.Count),
                AveragePopulation = server.Matches.Average(m => m.Scores.Count),
                Top5GameModes = server.Matches.GroupBy(m => m.GameMode).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToArray(),
                Top5Maps = server.Matches.GroupBy(m => m.Map).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToArray()
            };
        }

        public static PopularServerInfo CreatePopularServerInfo(this Server server, DateTime lastTimestamp)
        {
            var serverAge = server.Matches.Any()
                ? lastTimestamp.AddDays(1).Date - server.Matches.Min(m => m.Timestamp).Date
                : new TimeSpan(1, 0, 0, 0);
            return new PopularServerInfo
            {
                Endpoint = server.Endpoint,
                Name = server.Name,
                AverageMatchesPerDay = (double)server.Matches.Count / serverAge.Days
            };
        }
    }
}
