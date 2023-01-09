using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Data.DataModels
{
    public class Game
    {
        public Game()
        {
            UserGames = new HashSet<AppUserGame>();
        }
        [Key]
        public int Id { get; set; }
        public TimeSpan Duration { get; set; }    
        public DateTime GameStartTime { get; set; }
        public DateTime GameFinishTime { get; set; }
        public int? GoldEarned { get; set; }
        public virtual ICollection<AppUserGame> UserGames { get; set; }
    }
}
