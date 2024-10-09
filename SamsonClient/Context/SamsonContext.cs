using Microsoft.EntityFrameworkCore;
using SamsonClient.Models.Spotify;

namespace SamsonClient.Context
{
    public class SamsonContext : DbContext
    {
        public DbSet<SpotifyUserAuth> spotifyUserAuths { get; set; }

        public SamsonContext(DbContextOptions<SamsonContext> options) : base(options)
        {
        }
    }
}
