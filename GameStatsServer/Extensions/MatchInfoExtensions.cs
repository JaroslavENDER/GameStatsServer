using GameStatsServer.Entities;
using GameStatsServer.Models;
using System;
using System.Linq;

namespace GameStatsServer.Extensions
{
    public static class MatchInfoExtensions
    {
        public static Match CreateMatch(this MatchInfo matchInfo, string timestamp)
        {
            var result = new Match
            {
                Timestamp = DateTime.Parse(timestamp),
                Map = matchInfo.Map,
                GameMode = matchInfo.GameMode,
                FragLimit = matchInfo.FragLimit,
                TimeLimit = matchInfo.TimeLimit,
                TimeElapsed = matchInfo.TimeElapsed
            };
            result.Scores.AddRange(matchInfo.Scoreboard.Select(sb => new Score
            {
                PlayerName = sb.Name,
                Frags = sb.Frags,
                Kills = sb.Kills,
                Deaths = sb.Deaths
            }));
            for (var i = 0; i < result.Scores.Count; i++)
                result.Scores[i].LeaderboardPlace = i + 1;
            return result;
        }
    }
}
