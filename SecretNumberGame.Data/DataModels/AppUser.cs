using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SecretNumberGame.Data.DataModels
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            UserGames = new HashSet<AppUserGame>();
        }

        [Required]
        [StringLength(21)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(21)]
        public string? LastName { get; set; }
        public virtual ICollection<AppUserGame> UserGames { get; set; }
    }
}
