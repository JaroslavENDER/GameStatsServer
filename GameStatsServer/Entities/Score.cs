using System.ComponentModel.DataAnnotations;

namespace GameStatsServer.Entities
{
    public class Score
    {
        [Key] public int Id { get; set; }
        [Required] public string PlayerName { get; set; }
        [Required] public int Frags { get; set; }
        [Required] public int Kills { get; set; }
        [Required] public int Deaths { get; set; }
        [Required] public int LeaderboardPlace { get; set; }

        [Required] public Match Match { get; set; }
    }
}
