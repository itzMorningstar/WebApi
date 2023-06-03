using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Accounts
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountGuid { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Salt { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneVerified { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime? UpdateOn { get; set; }

        [StringLength(50)]
        public string? LastLogin { get; set; }

        [StringLength(15)]
        public string? LastLoginIP { get; set; }

        [StringLength(100)]
        public string? LastLoginUserAgent { get; set; }

        [StringLength(100)]
        public string? LastLoginLocation { get; set; }

        [StringLength(50)]
        public string? LastLoginDevice { get; set; }

        [StringLength(50)]
        public string? LastLoginOS { get; set; }

        [StringLength(50)]
        public string? LastLoginBrowser { get; set; }

        [Required]
        public Guid ApiKey { get; set; }

        [StringLength(50)]
        public string? ApiKeyUpdatedOn { get; set; }

    }
}
