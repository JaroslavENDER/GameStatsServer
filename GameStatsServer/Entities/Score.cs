using System.ComponentModel.DataAnnotations;

namespace GameStatsServer.Entities
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        public string PlayerName { get; set; }
            //{ get => Player.Name; }
        public int Frags { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }

        public virtual Match Match { get; set; }
        //public virtual Player Player { get; set; }
    }
}
