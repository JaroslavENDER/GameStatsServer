using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStatsServer.Entities
{
    public class Match
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime Timestamp { get; set; }
        [Required] public string Map { get; set; }
        [Required] public string GameMode { get; set; }
        [Required] public int FragLimit { get; set; }
        [Required] public int TimeLimit { get; set; }
        [Required] public double TimeElapsed { get; set; }

        [Required] public Server Server { get; set; }

        public List<Score> Scores { get; set; }

        public Match() => Scores = new List<Score>();
    }
}
