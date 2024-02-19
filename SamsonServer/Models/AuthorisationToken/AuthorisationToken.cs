using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SamsonServer.Models.AuthorisationToken
{
    public class AuthorisationToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
