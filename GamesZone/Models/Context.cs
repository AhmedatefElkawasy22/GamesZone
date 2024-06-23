using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamesZone.Models
{
	public class Context : IdentityDbContext
	{
		public Context() { }
		public Context(DbContextOptions<Context> options) :base(options) { }

		public DbSet<Game> Game { get; set; }
		public DbSet<Device> Device { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<GameDevice> GameDevice { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<GameDevice>().HasKey(d => new { d.DeviceId, d.GameId });
			base.OnModelCreating(builder);
		}
	}
}
