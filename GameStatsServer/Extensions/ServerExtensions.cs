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
                Info = new ServerInfo
                {
                    Name = server.Name,
                    GameModes = server.GameModes.Split(", ")
                }
            };
        }

        public static ServerStats CreateServerStats(this Server server, DateTime lastTimestamp)
        {
            var serverAge = lastTimestamp.AddDays(1).Date - server.Mathes.Min(m => m.Timestamp).Date;
            return new ServerStats
            {
                TotalMatchesPlayed = server.Mathes.Count,
                MaximunMatchesPerDay = server.Mathes.GroupBy(m => m.Timestamp.Date).Max(g => g.Count()),
                AverageMatchesPerDay = (double)server.Mathes.Count / serverAge.Days,
                MaximunPopulation = server.Mathes.Max(m => m.Scores.Count),
                AveragePopulation = server.Mathes.Average(m => m.Scores.Count),
                Top5GameModes = new string[0],//TODO:Top5GameModes
                Top5Maps = server.Mathes.GroupBy(m => m.Map).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToArray()
            };
        }
    }
}
