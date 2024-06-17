using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SamsonCommon.Enums;

namespace SamsonServer.Models.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Permission {  get; set; }
        
        public ListenMode ListenMode { get; set; }
        
        public int ListenDuration { get; set; }
    }
}
