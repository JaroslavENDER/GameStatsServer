namespace GameStatsServer.Models
{
    public class MatchInfo
    {
        public string Map { get; set; }
        public string GameMode { get; set; }
        public int FragLimit { get; set; }
        public int TimeLimit { get; set; }
        public double TimeElapsed { get; set; }
        public Score[] Scoreboard { get; set; }

        public class Score
        {
            public string Name { get; set; }
            public int Frags { get; set; }
            public int Kills { get; set; }
            public int Deaths { get; set; }
        }
    }
}
