using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStatsServer.Entities
{
    public class Match
    {
        [Key]
        public string Endpoint { get => Server.Endpoint; }
        [Key]
        public DateTime Timestamp { get; set; }
        public string Map { get; set; }
        public string GameMode { get; set; }
        public int FragLimit { get; set; }
        public int TimeLimit { get; set; }
        public double TimeElapsed { get; set; }

        public virtual Server Server { get; set; }
        public virtual List<Score> Scores { get; }

        public Match() => Scores = new List<Score>();
    }
}
