using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretNumberGame.Data.DataModels;

namespace SecretNumberGame.Data
{
    public class SecretDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public SecretDbContext(DbContextOptions<SecretDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }          
        public DbSet<AppUserGame> AppUserGames { get; set; }          
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUserGame>().HasKey(ug => new {ug.UserId, ug.GameId }); // Composite PKs
            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();
            builder.Entity<AppUser>()
                .Property(p => p.Email)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}