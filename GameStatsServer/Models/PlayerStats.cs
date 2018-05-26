namespace GameStatsServer.Models
{
    public class PlayerStats
    {
        public int TotalMatchesPlayed { get; set; }
        public int TotalMatchesWon { get; set; }
        public string FavoriteServer { get; set; }
        public int UniqueServers { get; set; }
        public string FavoriteGameMode { get; set; }
        public int MaximumMathcesPerDay { get; set; }
        public double AverageMatchesPerDay { get; set; }
        public string LastMatchPlayed { get; set; }
        public double KillToDeathRatio { get; set; }
    }
}
