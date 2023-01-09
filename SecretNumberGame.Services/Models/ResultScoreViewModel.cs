using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Models
{
    public class ResultScoreViewModel
    {       
        public Guid UserId { get; set; } 
        public string SecretNumber { get; set; } = null!;
        public DateTime StartGameTime { get; set; }
        public DateTime EndGameTime { get; set; }
        public TimeSpan Duration => this.EndGameTime - this.StartGameTime;        
        public int PenaltyPoints { get; set; }
    }
}
