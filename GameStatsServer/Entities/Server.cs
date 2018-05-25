using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStatsServer.Entities
{
    public class Server
    {
        [Key] public string Endpoint { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string GameModes { get; set; }

        public List<Match> Matches { get; set; }

        public Server() => Matches = new List<Match>();
    }
}
