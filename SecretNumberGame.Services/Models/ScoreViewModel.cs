using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Models
{
    public class ScoreViewModel
    {
        public int UserTotalScore { get; set; }
        public int UserTotalGames { get; set; }
        public int AveragePointsPerGame { get; set; }
        public int MaxGamePoints { get; set; }
        public bool HasBestTotalScore { get; set; }
    }
}
