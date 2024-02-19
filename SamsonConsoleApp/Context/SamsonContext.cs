using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.Context
{
    public class SamsonContext : DbContext
    {
        public DbSet<SpotifyUserAuth> spotifyUserAuths { get; set; }

        public SamsonContext(DbContextOptions<SamsonContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class SamsonContextFactory : IDesignTimeDbContextFactory<SamsonContext>
    {
        public SamsonContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SamsonContext>();
            optionsBuilder.UseSqlite("Data Source=samsonDB.db");

            return new SamsonContext(optionsBuilder.Options);
        }
    }
}
