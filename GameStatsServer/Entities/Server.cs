using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStatsServer.Entities
{
    public class Server
    {
        [Key]
        public string Endpoint { get; set; }
        public string Name { get; set; }
        public List<string> GameModes { get; set; }

        public virtual List<Match> Mathes { get; }

        public Server() => Mathes = new List<Match>();
    }
}
