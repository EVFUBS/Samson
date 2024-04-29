using SamsonCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonCommon.Helpers
{
    public class CatergoryHelper
    {
        public static Catergories GetCatergory(Actions action)
        {
            if (action >= Actions.GeneralStart && action <= Actions.GeneralEnd)
            {
                return Catergories.General;
            }

            if (action >= Actions.SpotifyStart && action <= Actions.SpotifyEnd)
            {
                return Catergories.Spotify;
            }

            return Catergories.DidNotUnderstand;
        }
    }
}
