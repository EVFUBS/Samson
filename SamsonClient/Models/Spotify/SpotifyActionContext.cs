using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonClient.Models.Spotify
{
    public record SpotifyActionContext
    {
        public SpotifyActionSongContext? SongContext { get; set; }
    }

    public record SpotifyActionSongContext
    {
        public string? Artist { get; set; }
        public string? Song { get; set; }
    }
}
