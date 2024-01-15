using Microsoft.EntityFrameworkCore;
using SamsonConsoleApp.Context;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.DAL
{
    public class SpotifyDAL : ISpotifyDAL
    {
        public SpotifyDAL() { 
        }

        public SpotifyUserAuth AddAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            using (var context = new SamsonContext())
            {
                context.spotifyUserAuths.Add(spotifyUserAuth);
                context.SaveChanges();
            }

            return spotifyUserAuth;
        }

        public SpotifyUserAuth UpdateAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            using (var context = new SamsonContext())
            {
                context.spotifyUserAuths.Update(spotifyUserAuth);
                context.SaveChanges();
            }

            return spotifyUserAuth;
        }

        public async Task<SpotifyUserAuth> GetAccessToken()
        {
            SpotifyUserAuth spotifyUser;

            using (var context = new SamsonContext())
            {
                var value = await context.spotifyUserAuths.OrderByDescending(x => x.Expires_at).FirstOrDefaultAsync();

                if (value != null)
                {
                    spotifyUser = value;
                    return spotifyUser;
                }

                throw new Exception("There are no AccessToken to get");
            }
        }

        public void RemoveAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            using (var context = new SamsonContext())
            {
                context.Remove(spotifyUserAuth);
            }
        }
    }
}
