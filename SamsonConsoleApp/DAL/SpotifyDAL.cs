using Microsoft.EntityFrameworkCore;
using SamsonConsoleApp.Context;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models.Spotify;
using SQLitePCL;

namespace SamsonConsoleApp.DAL
{
    public class SpotifyDAL : ISpotifyDAL
    {
        public SamsonContext _context {  get; set; }

        public SpotifyDAL(SamsonContext context) {
            _context = context;
        }

        public SpotifyUserAuth AddAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            _context.spotifyUserAuths.Add(spotifyUserAuth);
            _context.SaveChanges();
            return spotifyUserAuth;
        }

        public SpotifyUserAuth UpdateAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            _context.spotifyUserAuths.Update(spotifyUserAuth);
            _context.SaveChanges();
            return spotifyUserAuth;
        }

        public async Task<SpotifyUserAuth> GetAccessToken()
        {
            var value = await _context.spotifyUserAuths.OrderByDescending(x => x.Expires_at).FirstOrDefaultAsync();

            if (value != null)
            {
                return value;
            }

            throw new Exception("There are no AccessToken to get");   
        }

        public void RemoveAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            _context.spotifyUserAuths.Remove(spotifyUserAuth);
            _context.SaveChanges();
        }
    }
}
