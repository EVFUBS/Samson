using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SamsonServer.Models.AuthorisationToken;
using SamsonServer.Models.User;

namespace SamsonConsoleApp.Context
{
    public class SamsonContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthorisationToken> AuthorisationTokens { get; set; }

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
        private readonly IConfiguration _configuration;

        public SamsonContextFactory(IConfiguration config)
        {
            _configuration = config;
        }

        public SamsonContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SamsonContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Samsondb"));

            return new SamsonContext(optionsBuilder.Options);
        }
    }
}
