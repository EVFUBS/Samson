using SamsonCommon.Enums;

namespace SamsonCommon.Helpers
{
    public abstract class CategoryHelper
    {
        public static Catergories GetCategory(Actions action)
        {
            return action switch
            {
                >= Actions.GeneralStart and <= Actions.GeneralEnd => Catergories.General,
                >= Actions.SpotifyStart and <= Actions.SpotifyEnd => Catergories.Spotify,
                _ => Catergories.DidNotUnderstand
            };
        }
    }
}
