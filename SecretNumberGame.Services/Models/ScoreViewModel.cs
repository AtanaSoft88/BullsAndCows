using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Models
{
    public class ScoreViewModel
    {
        public string UserNames { get; set; }
        public int TotalGold { get; set; }
        public int UserTotalGames { get; set; }
        public double AverageGoldEarned { get; set; }
        public string BestDuration { get; set; }
        public bool HasBestTotalScore { get; set; }
        public bool HasBestTime { get; set; }       
        public List<int?> GoldAllGames { get; set; } 
    }
}
