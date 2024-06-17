using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SamsonServer.Models.AuthorisationToken;
using SamsonServer.Models.User;

namespace SamsonServer.Context
{
    public class SamsonContext(DbContextOptions<SamsonContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthorisationToken> AuthorisationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class SamsonContextFactory(IConfiguration config) : IDesignTimeDbContextFactory<SamsonContext>
    {
        public SamsonContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SamsonContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Samsondb"));

            return new SamsonContext(optionsBuilder.Options);
        }
    }
}
