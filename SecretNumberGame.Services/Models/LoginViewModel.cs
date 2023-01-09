using System.ComponentModel.DataAnnotations;

namespace SecretNumberGame.Services.Models
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        //Must be hidden
        // Into View.cshtml --->  <input type="hidden" asp-for="ReturnUrl"/>
        public string? ReturnUrl { get; set; }

    }
}
