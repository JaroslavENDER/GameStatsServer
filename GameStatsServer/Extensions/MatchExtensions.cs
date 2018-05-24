using GameStatsServer.Entities;
using GameStatsServer.Models;
using System.Linq;

namespace GameStatsServer.Extensions
{
    public static class MatchExtensions
    {
        public static MatchInfo CreateMatchInfo(this Match match)
        {
            return new MatchInfo
            {
                Map = match.Map,
                GameMode = match.GameMode,
                FragLimit = match.FragLimit,
                TimeLimit = match.TimeLimit,
                TimeElapsed = match.TimeElapsed,
                Scoreboard = match.Scores.Select(s => new MatchInfo.Score
                {
                    Name = s.PlayerName,
                    Frags = s.Frags,
                    Kills = s.Kills,
                    Deaths = s.Deaths
                }).ToArray()
            };
        }
    }
}
