using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Data.DataModels
{
    public class AppUserGame
    {
        [ForeignKey(nameof(AppUser))]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }


        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
