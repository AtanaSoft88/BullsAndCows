using System.ComponentModel.DataAnnotations;

namespace SecretNumberGame.Services.Models
{
    public class SecretNumberViewModel
    {
        public string SecretNum { get; set; } = null!;
        public int Col { get; set; }
        public string Num { get; set; } = null!;
        public string NumFigure { get; set; } = null!;
        public DateTime StartGameTime => DateTime.Now;
    }
}
