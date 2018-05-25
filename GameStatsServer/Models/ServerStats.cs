namespace GameStatsServer.Models
{
    public class ServerStats
    {
        public int TotalMatchesPlayed { get; set; }
        public int MaximunMatchesPerDay { get; set; }
        public double AverageMatchesPerDay { get; set; }
        public int MaximunPopulation { get; set; }
        public double AveragePopulation { get; set; }
        public string[] Top5GameModes { get; set; }
        public string[] Top5Maps { get; set; }
    }
}
