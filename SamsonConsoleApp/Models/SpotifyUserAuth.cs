using SamsonConsoleApp.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamsonConsoleApp.Models
{
    public class SpotifyUserAuth : ISpotifyUserAuth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Scope { get; set; }
        public int Expires_in { get; set; }
        public DateTimeOffset Expires_at { get; set; } = DateTimeOffset.UtcNow;
        public string Refresh_token { get; set; }
    }
}
