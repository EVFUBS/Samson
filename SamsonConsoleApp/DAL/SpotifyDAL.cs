using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Context;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                spotifyUser = await context.FindAsync<SpotifyUserAuth>(1);
            }

            return spotifyUser;
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
