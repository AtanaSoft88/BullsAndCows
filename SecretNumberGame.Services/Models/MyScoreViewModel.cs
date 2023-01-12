using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNumberGame.Services.Models
{
    public class MyScoreViewModel
    {
        public string UserNames { get; set; }
        public string Email { get; set; }
        public string GameDuration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? GoldForTheGame { get; set; }
        public string SecretNumber { get; set; }
    }
}
