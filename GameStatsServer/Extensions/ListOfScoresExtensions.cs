using GameStatsServer.Entities;
using GameStatsServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStatsServer.Extensions
{
    public static class ListOfScoresExtensions
    {
        public static PlayerStats CreateStats(this List<Score> scores, DateTime lastTimestamp)
        {
            var playerAge = lastTimestamp.AddDays(1).Date - scores.Min(m => m.Match.Timestamp).Date;
            return new PlayerStats
            {
                TotalMatchesPlayed = scores.Count,
                TotalMatchesWon = scores.Count(s => s.LeaderboardPlace == 1),
                FavoriteServer = scores.GroupBy(s => s.Match.Server).OrderByDescending(g => g.Count()).First().Key.Endpoint,
                UniqueServers = scores.GroupBy(s => s.Match.Server).Count(),
                FavoriteGameMode = scores.GroupBy(s => s.Match.GameMode).OrderByDescending(g => g.Count()).First().Key,
                MaximumMathcesPerDay = scores.GroupBy(s => s.Match.Timestamp.Date).Max(g => g.Count()),
                AverageMatchesPerDay = (double)scores.Count / playerAge.Days,
                LastMatchPlayed = scores.Max(s => s.Match.Timestamp).ToString(),
                KillToDeathRatio = (double)scores.Sum(s => s.Kills) / scores.Sum(s => s.Deaths)
            };
        }
    }
}
