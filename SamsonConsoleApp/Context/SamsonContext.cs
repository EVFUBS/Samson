using Microsoft.EntityFrameworkCore;
using SamsonConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Context
{
    public class SamsonContext : DbContext
    {
        public DbSet<SpotifyUserAuth> spotifyUserAuths { get; set; }

        public SamsonContext(): base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=samsonDB.db"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
