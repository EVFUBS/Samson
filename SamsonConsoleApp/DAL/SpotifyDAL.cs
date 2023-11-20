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
        public SpotifyDAL() { }

        public void AddAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            using (var context = new SamsonContext())
            {
                context.Add(spotifyUserAuth);
            }

        }
    }
}
