using Microsoft.EntityFrameworkCore;

namespace Database {
	public class Context : DbContext {
		public DbSet<Player> Players { get; set; }
		public DbSet<Pet> Pets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlite($"Data Source={Settings.Get("DatabasePath")};");
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Player>().ToTable("Players");
			modelBuilder.Entity<Pet>().ToTable("Pets");
		}
	}
}