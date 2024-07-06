using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamesZone.Models
{
    public class Context : IdentityDbContext<User>
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Game> Game { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<GameDevice> GameDevice { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<GameDevice>().HasKey(d => new { d.DeviceId, d.GameId });
            builder.Entity<Game>().Property(d => d.UserId).HasDefaultValue("0f6be9f7-82c1-4add-8c15-c749a72c9206");
            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = "Admin" }
                );
        }
    }
}
