using Microsoft.EntityFrameworkCore;
using SamsonLocal.Context;
using SamsonLocal.DAL.interfaces;
using SamsonLocal.Models.Spotify;

namespace SamsonLocal.DAL
{
    public class SpotifyDal(SamsonContext context) : ISpotifyDal
    {
        private SamsonContext context {  get; set; } = context;

        public SpotifyUserAuth AddAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            context.spotifyUserAuths.Add(spotifyUserAuth);
            context.SaveChanges();
            return spotifyUserAuth;
        }

        public SpotifyUserAuth UpdateAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            context.spotifyUserAuths.Update(spotifyUserAuth);
            context.SaveChanges();
            return spotifyUserAuth;
        }

        public async Task<SpotifyUserAuth> GetAccessToken()
        {
            var value = await context.spotifyUserAuths.OrderByDescending(x => x.Expires_at).FirstOrDefaultAsync();

            if (value != null)
            {
                return value;
            }

            throw new Exception("There are no AccessToken to get");   
        }

        public void RemoveAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            context.spotifyUserAuths.Remove(spotifyUserAuth);
            context.SaveChanges();
        }
    }
}
