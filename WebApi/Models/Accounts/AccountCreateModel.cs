using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Accounts
{
    public class AccountCreateModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public string Email { get; set; }

        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

    }
}
