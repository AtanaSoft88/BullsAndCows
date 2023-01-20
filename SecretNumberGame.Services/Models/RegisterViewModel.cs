using SecretNumberGame.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace SecretNumberGame.Services.Models
{
    public class RegisterViewModel
    {      

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [StringLength(21, MinimumLength = 2)]
        [RegularExpression(@"\b[A-Z]{1}[a-z]{1,20}\b", ErrorMessage = GlobalConstants.REGISTER_ERROR_MSG)]
        public string FirstName { get; set; } = null!;


        [Required]
        [StringLength(21, MinimumLength = 2)]
        [RegularExpression(@"\b[A-Z]{1}[a-z]{1,20}\b", ErrorMessage = GlobalConstants.REGISTER_ERROR_MSG)]
        public string LastName { get; set; } = null!;
    }
}
