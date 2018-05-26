namespace GameStatsServer.Models
{
    public class MatchInfoWithEndpointAndTimestamp
    {
        public string Server { get; set; }
        public string Timestamp { get; set; }
        public MatchInfo Results { get; set; }
    }
}
